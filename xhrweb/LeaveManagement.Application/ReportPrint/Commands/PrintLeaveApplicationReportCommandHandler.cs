
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Services;
using Attendance.Application.ReportPrint.Model;
using CompanySetup.Application.CompanyAddress.Queries.Models;
using Dapper;
using LeaveManagement.Application.ReportPrint.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.ReportPrint.Commands
{
    public class PrintLeaveApplicationReportCommandHandler : IRequestHandler<PrintLeaveApplicationReportCommand, string>
    {
        #region ctor
        public PrintLeaveApplicationReportCommandHandler(IConfiguration configuration, DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;
            //_dataContext = dataContext;
        }
        #endregion

        #region properties
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        #endregion

        #region methods
        public async Task<string> Handle(PrintLeaveApplicationReportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                var query = "";
                // Conver date
                DateTime formatEndDate = (DateTime)request?.EndDate;
                string endDate = formatEndDate.ToString("yyyy-MM-dd",CultureInfo.InvariantCulture);
                DateTime formatStartDate = (DateTime)request.StartDate;
                string startDate = formatStartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                string end = formatEndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                string start = formatStartDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                query = $"SELECT * from leave.RPTgetleaveapplicationbyParameter('{request.CompanyId}','{startDate}','{endDate}','{request.EmployeeName}','{request.LeaveTypeName}','{request.ApprovalStatusText}')";
                query = query.Replace("'undefined'", "null", StringComparison.OrdinalIgnoreCase);
                query = query.Replace("''", "null", StringComparison.OrdinalIgnoreCase);
                query = query.Replace("All", "null", StringComparison.OrdinalIgnoreCase);
                query = query.Replace("'null'", "null", StringComparison.OrdinalIgnoreCase);
                var data = await _connection.QueryAsync<ReportLeaveApplicationPdfVM>(query).ConfigureAwait(false);
                var leaveApplicationData = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery).ConfigureAwait(false);

                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery).ConfigureAwait(false);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();

                var consolidatedChildren = from c in leaveApplicationData
                                           group c by new { c.CompanyName } into gcs

                                           select new GroupLeaveApplicationPdfVM()
                                           {


                                               CompanyName = !companyData.Any() ? gcs.Key.CompanyName : companyData.FirstOrDefault().CompanyName,
                                               CompanyAddress = !companyData.Any() ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               CompanyLogoUri = (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && leaveApplicationData.Any()) == true ? _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri):null,
                                               StartDate = start,
                                               EndDate = end,
                                               DataList = gcs.ToList(),

                                           };


                
                var docName = string.Format("Leave_Rep_{0}.pdf", DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture));
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintLeaveApplicationReport.cshtml", consolidatedChildren).ConfigureAwait(false);

                ReportService reportService = new ReportService()
                {
                    OutPath = rootPath + dataFilePath + docName,
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView,
                    Orientation = DinkToPdf.Orientation.Portrait,
                    Margins = new DinkToPdf.MarginSettings(2, 2, 2, 2),
                    PaperKind = DinkToPdf.PaperKind.A4
                };
                request.Converter.Convert(reportService.PdfDocument);
                //docName = "Xtra_Gift_Card.pdf";
                string printGiftPath = $"{dataFilePath}{docName}";
                return printGiftPath;

            }

            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion

    }
}
