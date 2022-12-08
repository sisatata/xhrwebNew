using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using PayrollManagement.Application.ReportPrshort.Model;
using PayrollManagement.Application.ReportPrint.Model;
using System.Linq;
using Attendance.Application.ReportPrint.Model;
using ASL.Hrms.SharedKernel.Services;
using System.IO;
using ASL.Utility.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;

namespace PayrollManagement.Application.ReportPrint.Commands
{
    public class PrintBonusSheetCommandHandler : IRequestHandler<Commands.V1.PrintBonusReport, string>
    {
        #region ctor
        public PrintBonusSheetCommandHandler(DbConnection connection, IConfiguration configuration, IUriComposer uriComposer)
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
        private readonly int year = 365;
        private readonly int month = 30;

        #endregion
        #region methods
        public async Task<string> Handle(Commands.V1.PrintBonusReport request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                if (request.EmployeeId == null || request.EmployeeId == Guid.Empty )
                query = $"SELECT * from payroll.RPTGetBonusSheetByCompanyAndBonusType('{request.CompanyId}','{request.FinancialYearId}','{request.BonusTypeId}')";
                else
                    query = $"SELECT * from payroll.RPTGetBonusSheetByEmployee('{request.CompanyId}','{request.FinancialYearId}','{request.BonusTypeId}','{request.EmployeeId}')";
                var data = await _connection.QueryAsync<ReportBonusSheetPdfVM>(query);
                var bonus = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                // Calculate employee service length
                foreach (var item in bonus)
                {
                    var joinigDate = new DateTime(item.JoiningDate.Year, item.JoiningDate.Month, item.JoiningDate.Day);
                    var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    var dateDiff = today.Subtract(joinigDate);
                    var date = string.Format("{0} Y {1} M {2} D", (int)dateDiff.TotalDays / year,
                      (int)(dateDiff.TotalDays % year) / month, (int)(dateDiff.TotalDays % year) % month);
                    item.ServiceLength = date;
                }
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();
                var consolidatedChildren = from c in bonus
                                           group c by new { c.FinancialYearName, c.LookUpTypeName } into gcs
                                           select new GroupedBonusSheetPdfVM()
                                           {

                                               CompanyName = companyData.Count() == 0 ? data.FirstOrDefault().CompanyName : companyData.FirstOrDefault().CompanyName,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               LookUpTypeName = gcs.Key.LookUpTypeName,
                                               FinancialYearName = gcs.Key.FinancialYearName,
                                               CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && bonus != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null,
                                               TotalNetPayableBonusInWord = InWordWithinBangla.ConvertNumbertoEnglishWords(Convert.ToInt32(gcs.ToList().Sum(x => x.PayableBonus))),
                                               DataList = gcs.ToList(),
                                           };
                var bonusSheetData = consolidatedChildren.ToList().FirstOrDefault();
                var docName = string.Format("Bonus_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintBonusReport.cshtml", bonusSheetData);
                ReportService reportService = new ReportService()
                {
                    OutPath = $"{rootPath}{dataFilePath}{docName}",  //   rootPath + dataFilePath + docName, 
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView,
                    Orientation = DinkToPdf.Orientation.Landscape,
                    PaperKind = DinkToPdf.PaperKind.Legal
                };
                request.Converter.Convert(reportService.PdfDocument);
                //docName = "Xtra_Gift_Card.pdf";
                string printPath = $"{dataFilePath}{docName}";
                return printPath;
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
        #endregion
    }
}
