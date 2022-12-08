using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
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
using static PayrollManagement.Application.ReportPrint.Commands.Commands.V1;

namespace PayrollManagement.Application.ReportPrint.Commands
{
    public class PrintEarnLeaveEncashmentReportCommandHandler : IRequestHandler<PrintEarnLeaveEncashmentReport, string>
    {
        public PrintEarnLeaveEncashmentReportCommandHandler(DbConnection connection, IConfiguration configuration, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;

        }
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        public async Task<string> Handle(PrintEarnLeaveEncashmentReport request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                query = $"SELECT * from payroll.RPTEarnLeaveEncashmentByCompanyId('{request.CompanyId}', '{request.FinancialYearId}', '{request.MonthCycleId}')";
                var data = await _connection.QueryAsync<ReportLeaveEncashmentPdfVM>(query);
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                data = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();
                var consolidatedChildren = from c in data
                                           group c by new { c.FinancialYearName } into gcs
                                           select new GroupedEarnLeaveEncashmentPdfVM()
                                           {
                                               CompanyName = companyData.FirstOrDefault().CompanyName,
                                               CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && data != null) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri) : null,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               DataList = gcs.ToList(),
                                               FinancialYearName = gcs.Key.FinancialYearName,
                                               MonthCycleName = gcs.FirstOrDefault().MonthCycleName,
                                              
                                           };

              
                var docName = string.Format("Sal_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintEarnLeaveEncashmentReport.cshtml", consolidatedChildren.FirstOrDefault());
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
            catch( Exception ex)
            {
                throw new ArgumentNullException(ex.ToString());
            }
        }
    }
}
