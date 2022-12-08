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
    public class PrintOTSummaryReportCommandHandler : IRequestHandler<Commands.V1.PrintOTSummaryReport, string>
    {
        #region ctor
        public PrintOTSummaryReportCommandHandler(DbConnection connection, IConfiguration configuration, IUriComposer uriComposer)
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
        public async Task<string> Handle(Commands.V1.PrintOTSummaryReport request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                var format = (DateTime)request.EndDate;
                var endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartDate;
                var startDate = format.ToString("yyyy-MM-dd");
                query = $"SELECT * from payroll.RPTOTSummaryByCompanyAndDateRange('{request.CompanyId}', '{startDate}', '{endDate}')";
                var data = await _connection.QueryAsync<ReportOTSummayPdfVM>(query);
                var OTSummary = data.OrderBy(x => x.DepartmentName).ToList();
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
                query = $"SELECT * from main.GetCompanyLogoUri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<CompanyLogoUriVM>(query);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();
                string logoUri = "";
                if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && OTSummary.Count>0)
                {

                    logoUri = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
                }
                var consolidatedChildren = from c in OTSummary
                                           group c by new { c.CompanyId, c.DepartmentId,c.DepartmentName} into gcs
                                           select new GroupedReportOTSummaryPdfVM()
                                           {

                                               CompanyName = companyData.Count() == 0 ? data.FirstOrDefault().CompanyName : companyData.FirstOrDefault().CompanyName,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               Hours = gcs.Sum(x=>x.Hours),
                                               Minutes = gcs.Sum(x=>x.Minutes),
                                               StartDate = request.StartDate.ToString("dd/MM/yyy"),
                                               EndDate = request.EndDate.ToString("dd/MM/yyy"),
                                               DepartmentName = gcs.Key.DepartmentName,
                                               CompanyLogoUri = logoUri,
                                               TotalOTRate = gcs.Sum(x=>x.TotalOTRate),
                                           };
                var bonusSheetData = consolidatedChildren.ToList();
                var netPayableOT = InWordWithinBangla.ConvertNumbertoEnglishWords(Convert.ToInt32(bonusSheetData.Sum(x => x.TotalOTRate)));
                bonusSheetData.FirstOrDefault().NetPayableOT = netPayableOT;
                var docName = string.Format("OTSummary_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintOTSummaryReport.cshtml", bonusSheetData);
                ReportService reportService = new ReportService()
                {
                    OutPath = $"{rootPath}{dataFilePath}{docName}",  //   rootPath + dataFilePath + docName, 
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView,
                   
                };
                request.Converter.Convert(reportService.PdfDocument);
                string printPath = $"{dataFilePath}{docName}";
                return printPath;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.ToString());
            }

        }
        #endregion
    }
}    
