using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
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
    public class PrintProvidentFundReportCommandHandler : IRequestHandler<Commands.V1.PrintProvidentFundReport, string>
    {
        public PrintProvidentFundReportCommandHandler(IConfiguration configuration, DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;

        }
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        private readonly int year = 365;
        private readonly int month = 30;
        public async  Task<string> Handle(Commands.V1.PrintProvidentFundReport request, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
            var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
            var query = "";
            if (request.EmployeeId == null || request.EmployeeId == Guid.Empty)
                query = $"SELECT * from payroll.RPTGetProvidentFundByCompany('{request.CompanyId}','{request.FinancialYearId}','{request.MonthCycleId}')";
            else
                query = $"SELECT * from payroll.RPTGetProvidentFundByEmployee('{request.CompanyId}','{request.FinancialYearId}','{request.MonthCycleId}','{request.EmployeeId}')";

            var data = await _connection.QueryAsync<ReportProvidentFundPdfVM>(query);
            var providentFunds = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.SortOrder).ThenBy(x => x.FullName);
            // Calculate employee service length
            foreach (var item in providentFunds)
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
            var logoQuery = $"SELECT * from main.GetCompanyLogoUri('{request.CompanyId}')";
            var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
            var companyLogoUriData = companyLogoUri?.FirstOrDefault();

            var consolidatedChildren = from c in providentFunds
                                       group c by new { c.CompanyName } into gcs
                                       select new GroupedProvidentFundReportPdfVM()
                                       {
                                           CompanyName = companyData.Count() == 0 ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                           CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && providentFunds != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null,
                                           Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                           MonthCycleName = gcs.FirstOrDefault().MonthCycleName,
                                           FinancialYearName = gcs.FirstOrDefault().FinancialYearName,
                                           DataList = gcs.ToList()
                                           
                                       };

            var docName = string.Format("ProvidentFund_Report_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
            var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintProvidentFindReport.cshtml", consolidatedChildren.FirstOrDefault());
            ReportService reportService = new ReportService()
            {
                OutPath =    $"{rootPath}{dataFilePath}{docName}",   //rootPath + dataFilePath + docName,
                StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                HtmlContent = htmlView,
                Orientation = DinkToPdf.Orientation.Landscape,
                PaperKind = DinkToPdf.PaperKind.Legal

            };

            request.Converter.Convert(reportService.PdfDocument);
            string printPath = $"{dataFilePath}{docName}";
            return printPath;



        }
    }
}
