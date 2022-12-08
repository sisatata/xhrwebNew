using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Attendance.Application.ReportPrint.Model;
using CompanySetup.Application.CompanyAddress.Queries.Models;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models;
using PayrollManagement.Application.ReportPrint.Model;
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
   public class PrintPaySlipReportCommandHandler : IRequestHandler<PrintPaySlipReportCommand, string>
    {
        #region ctor
        public PrintPaySlipReportCommandHandler(IConfiguration configuration, DbConnection connection)
        {
            _connection = connection;
            _configuration = configuration;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        #endregion
        #region methods
        public async Task<string> Handle(PrintPaySlipReportCommand request, CancellationToken cancellationToken)
        {
            try{
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                if (request.EmployeeId == null || request.EmployeeId == Guid.Empty)
                {
                    query = $"SELECT * from payroll.RPTGetPaySlipByCompany('{request.CompanyId}','{request.FinancialYearId}','{request.MonthCycleId}')";
                   
                }
                else
                {
                    query = $"SELECT * from payroll.RPTGetPaySlipByEmployee('{request.CompanyId}','{request.EmployeeId}','{request.FinancialYearId}','{request.MonthCycleId}')";
                }
                var data = await _connection.QueryAsync<ReportPaySlipPdfVM>(query);
                // var paySlipData = data.ToList();
                var paySlipData = data.OrderBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var querySalaryComponent = $"SELECT * from payroll.RPTGetEmployeeSalaryCompnentsDetail('{request.CompanyId}')";
                var componentList = await _connection.QueryAsync<EmployeeSalaryComponentsDetailVM>(querySalaryComponent);
                var salaryComponentsDetail = componentList.ToList();
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
                var salaryIds = from c in paySlipData select new { c.SalaryId };
                var salaryWithComponent = from c in salaryComponentsDetail
                                          join d in salaryIds on c.EmployeeSalaryId equals d.SalaryId
                                          select new
                                          {
                                              c.ComponentAmount,
                                              c.EmployeeSalaryId,
                                              c.SalaryComponentName,
                                              c.Id
                                          };
                var SalaryComponentsData = salaryWithComponent.ToList();
                // manually insert data for basic, medical, houserent, conveyance and food
                foreach (var item in paySlipData)
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
                            item.TotalNetPayableSalaryInWord = InWordWithinBangla.ConvertNumbertoEnglishWords(Convert.ToInt32(item.NetPayableAmount));
                        }
                    }
                }
               // add deductions
                decimal? otheDeductionInt = 0.00M;
                decimal? absentDeductionInt = 0.00M;
                foreach (var item in paySlipData)
                {
                    otheDeductionInt = 0.00M;
                    var q = $"SELECT * from payroll.GetEmployeeSalaryProcessedDataComponentByParent ('{item.SalaryProcessedId}')";
                    var d = await _connection.QueryAsync<EmployeeSalaryProcessedDataComponentVM>(q);
                    var deductionList = d.Select(x => new { x.DisplayName, x.ComponentValue, x.Type }).Where(y => y.Type == "D");
                    if (deductionList.Count() > 0)
                    {
                        foreach (var deductionItem in deductionList)
                        {
                            if (string.Equals(deductionItem.DisplayName, "Absent Deduction", StringComparison.OrdinalIgnoreCase))
                                absentDeductionInt = Math.Round(deductionItem.ComponentValue.Value,2);
                            else
                            {
                                otheDeductionInt +=Math.Round(deductionItem.ComponentValue.Value,2);
                            }
                           
                        }
                        if(absentDeductionInt != null )
                        item.AbsentDeduction = absentDeductionInt.ToString();
                        if(otheDeductionInt != null)
                        item.OtherDeduction = otheDeductionInt.ToString();
                        if (absentDeductionInt == null)
                            item.AbsentDeduction = "";
                        if (otheDeductionInt == null)
                            item.OtherDeduction = "";

                    }
                }
               
                var consolidatedChildren = from c in paySlipData
                                           group c by new { c.CompanyName, c.MonthCycleName, c.FinancialYearName, c.Address } into gcs

                                           select new GroupedPaySlipPdfVM()
                                           {
                                               CompanyName = companyData.Count() == 0 ? data.FirstOrDefault().CompanyName : companyData.FirstOrDefault().CompanyName,
                                               Address = companyData.Count() == 0 ? "Company Address" : companyData.FirstOrDefault().CompanyAddress,
                                               MonthCycleName = gcs.Key.MonthCycleName,
                                               FinancialYearName = gcs.Key.FinancialYearName,
                                               DataList = gcs.ToList(),
                                             
                                           };
                var allPaySlipData = consolidatedChildren;
                var docName = string.Format("Payslip_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintPaySlipReport.cshtml", allPaySlipData);
                ReportService reportService = new ReportService()
                {
                    OutPath = $"{rootPath}{dataFilePath}{docName}" ,  //   rootPath + dataFilePath + docName, 
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView,
                    Orientation = DinkToPdf.Orientation.Portrait,
                    Margins= new DinkToPdf.MarginSettings(5,5,5,5),
                    PaperKind = DinkToPdf.PaperKind.A4
                };
                request.Converter.Convert(reportService.PdfDocument);
                string url = $"{dataFilePath}{docName}";
                return url;

            }

            catch(Exception ex)
            {
                 return ex.ToString();
            }
        }
        #endregion
    }
}
