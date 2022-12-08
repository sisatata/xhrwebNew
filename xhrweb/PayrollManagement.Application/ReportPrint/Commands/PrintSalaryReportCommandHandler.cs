using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Attendance.Application.ReportPrint.Model;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Application.ReportPrint.Model;
using PayrollManagement.Application.ReportPrshort.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace PayrollManagement.Application.ReportPrint.Commands
{
    public class PrintSalaryReportCommandHandler : IRequestHandler<PrintSalaryReportCommand, string>
    {
        #region ctor
        public PrintSalaryReportCommandHandler(IConfiguration configuration, DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        #endregion
        #region methods
        public async Task<string> Handle(PrintSalaryReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                if(request.EmployeeId == null || request.EmployeeId == Guid.Empty)
                query = $"SELECT * from payroll.RPTGetSalarySheetByCompany('{request.CompanyId}','{request.FinancialYearId}','{request.MonthCycleId}')";
                else
                    query = $"SELECT * from payroll.RPTGetSalarySheetByEmployee('{request.CompanyId}','{request.FinancialYearId}','{request.MonthCycleId}','{request.EmployeeId}')";
                var data = await _connection.QueryAsync<ReportSalarySheetPdfVM>(query);
                var salaryData = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);

                var querySalaryComponent = $"SELECT * from payroll.RPTGetEmployeeSalaryCompnentsDetail('{request.CompanyId}')";
                var componentList = await _connection.QueryAsync<EmployeeSalaryComponentsDetailVM>(querySalaryComponent);
                var salaryComponentsDetail = componentList.ToList();
                var salaryIds = from c in salaryData select new { c.SalaryId };
                var salaryWithComponent = from c in salaryComponentsDetail
                                          join d in salaryIds on c.EmployeeSalaryId equals d.SalaryId
                                          select new
                                          {
                                              c.ComponentAmount,
                                              c.EmployeeSalaryId,
                                              c.SalaryComponentName,
                                              c.Id
                                          };
                var SalaryComponentsData = salaryWithComponent.ToList().Distinct();
                
               
                // manually insert data for basic, medical, houserent, conveyance and food
                foreach (var item in salaryData)
                {
                    var curentSalary = item.SalaryId;
                    
                   
                    foreach (var itemComponent in salaryWithComponent)   
                    {
                        if (curentSalary == itemComponent.EmployeeSalaryId)
                        {
                            if (itemComponent.SalaryComponentName.ToUpper() == "BASIC")
                            {
                                item.Basic = itemComponent.ComponentAmount == null ? 0 : itemComponent.ComponentAmount;
                            }
                            else if (itemComponent.SalaryComponentName.ToUpper() == "MEDICAL")
                            {
                                item.Medical = itemComponent.ComponentAmount == null ? 0 : itemComponent.ComponentAmount;
                            }
                            else if (itemComponent.SalaryComponentName.ToUpper() == "HOUSE RENT" || itemComponent.SalaryComponentName.ToUpper() == "HOUSERENT")
                            {
                                item.HouseRent = itemComponent.ComponentAmount == null ? 0 : itemComponent.ComponentAmount;
                            }
                            else if (itemComponent.SalaryComponentName.ToUpper() == "CONVEYANCE")
                            {
                                item.Conveyance = itemComponent.ComponentAmount == null ? 0 : itemComponent.ComponentAmount;
                            }
                            else if (itemComponent.SalaryComponentName.ToUpper() == "FOOD")
                            {
                                item.Conveyance = itemComponent.ComponentAmount == null ? 0 : itemComponent.ComponentAmount;
                            }
                        }
                    }
                }
                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();
                var consolidatedChildren = from c in salaryData
                                           group c by new { c.CompanyName, c.MonthCycleName, c.FinancialYearName } into gcs
                                           select new GroupedSalarySheetPdfVM()
                                           {
                                               CompanyName = companyData.Count() == 0 ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                               CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && salaryData != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               MonthCycleName = gcs.Key.MonthCycleName,
                                               FinancialYearName = gcs.Key.FinancialYearName,
                                               DataList = gcs.ToList(),
                                               TotalNetPayableSalaryInWord = InWordWithinBangla.ConvertNumbertoEnglishWords(Convert.ToInt32(gcs.ToList().Sum(x => x.NetPayableAmount))),
                                           };
                var allSalaryData = consolidatedChildren.FirstOrDefault();
                var docName = string.Format("Sal_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintSalaryReport.cshtml", allSalaryData);
              
                ReportService reportService = new ReportService()
                {
                    OutPath = $"{rootPath}{dataFilePath}{docName}",  //   rootPath + dataFilePath + docName, 
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView,
                    Orientation = DinkToPdf.Orientation.Landscape,
                    PaperKind = DinkToPdf.PaperKind.Legal
                };
                request.Converter.Convert(reportService.PdfDocument);
                string url = $"{dataFilePath}{docName}";
                return url;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion
    }
}
