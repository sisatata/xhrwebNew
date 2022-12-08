
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Attendance.Application.ReportPrint.Model;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.ReportPrint.Commands
{
    public class PrintJobCardSummaryReportCommandHandler : IRequestHandler<PrintJobCardSummaryReportCommand, string>
    {
        #region ctor
        public PrintJobCardSummaryReportCommandHandler(IConfiguration configuration, DbConnection connection, IUriComposer uriComposer)
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
        public async Task<string> Handle(PrintJobCardSummaryReportCommand request, CancellationToken cancellationToken)
        {
            try {
                var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
                var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
                DateTime format = (DateTime)request.EndTime;
                string endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartTime;
                string startDate = format.ToString("yyyy-MM-dd");
                var query = "";
                query = $"SELECT * from attendance.RPTAttendanceSummaryByCompanyAndDateRange('{request.CompanyId}','{startDate}','{endDate}')";
                var data = await _connection.QueryAsync<ReportAttendanceSummaryPdfVM>(query);
                if (request.BranchIds != null && request.BranchIds.Count > 0)
                {
                    data = data.Where(r => request.BranchIds.Contains(r.BranchId.Value));
                }
                if (request.DepartmentIds != null && request.DepartmentIds.Count > 0)
                {
                    data = data.Where(r => request.DepartmentIds.Contains(r.DepartmentId.Value));
                }
                if (request.DesignationIds != null && request.DesignationIds.Count > 0)
                {
                    data = data.Where(r => request.DesignationIds.Contains(r.PositionId.Value));
                }
                if (request.EmployeeIds != null && request.EmployeeIds.Count > 0)
                {
                    data = data.Where(r => request.EmployeeIds.Contains(r.EID.Value));
                }



                var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
             
                data = data.OrderBy(x => x.BranchName).ThenBy(x => x.DepartmentName).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName);
                var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
                var oData = data.ToList();

                var consolidatedChildren = from c in oData
                                           group c by new { c.TotalOT, c.EmployeeId, c.DepartmentName, c.DesignationName, c.FullName, c.TotalAbsentDays, c.TotalLeaveDays, c.TotalPresentDays, c.JoiningDateString, c.TotalLateDays, c.EmployeeCode, c.BranchName } into gcs
                                           select new GroupedAttendanceSummaryPdfVM()
                                           {
                                               CompanyName = companyData.Count() == 0 ? data.FirstOrDefault().CompanyName : companyData.FirstOrDefault().CompanyName,
                                               Address = companyData.Count() == 0 ? "" : companyData.FirstOrDefault().CompanyAddress,
                                               BranchName = gcs.Key.BranchName,
                                               DepartmentName = gcs.Key.DepartmentName,
                                               DesignationName = gcs.Key.DesignationName,
                                               TotalAbsentDays = (short?)gcs.Count(x => x.Status == "A"),
                                               FullName = gcs.Key.FullName,
                                               JoiningDateString = gcs.Key.JoiningDateString,
                                               TotalLeaveDays = (short?)gcs.Count(x => x.Status == "CL" || x.Status == "SL"),
                                               TotalPresentDays = (short?)gcs.Count(x => x.Status == "P" || x.Status == "L"),
                                               TotalLateDays = (short?)gcs.Count(x => x.Status == "L"),
                                               EmployeeId = gcs.Key.EmployeeId,
                                               StartDate = request.StartTime.Value.ToString("dd/MM/yyyy"),
                                               EndDate = request.EndTime.Value.ToString("dd/MM/yyyy"),
                                               EmployeeCode = gcs.Key.EmployeeCode,
                                               DataList = gcs.ToList(),
                                           };
                var consolidatedChildrenList = consolidatedChildren.ToList();
               
                /*foreach (var item in consolidatedChildrenList.Select((value, i) => new { i, value }))
                {
                    TimeSpan ts = new TimeSpan();
                    foreach (var childItem in item.value.DataList)
                    {
                        var timeSpan = (childItem.OTHour.Value.ToString("HH:mm"));
                        TimeSpan time = TimeSpan.Parse(timeSpan);
                        ts = ts.Add(time);

                    }
                    var index = item.i;
                    consolidatedChildrenList[index].TotalOT = string.Format("{0:D1}:{1:D2}:{2:D2}", ts.Days, ts.Hours, ts.Minutes);

                }*/
                var logoQuery = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
                var companyLogoUri = await _connection.QueryAsync<ASL.Hrms.SharedKernel.Models.CompanyLogoUriVM>(logoQuery);
                var companyLogoUriData = companyLogoUri?.FirstOrDefault();
                if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && consolidatedChildrenList.Count() > 0)
                {

                    consolidatedChildrenList.FirstOrDefault().DataList[0].CompanyLogoUri = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
                }
                var docName = $"JobCardSummary_{DateTime.Now.ConvertToUnixEpochInSecond()}.pdf";// DateTime.Now.ToString("yyyyMMdd_HHmmss")
                var nn = consolidatedChildrenList;
                var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintJobCardSummaryReport.cshtml", consolidatedChildrenList);
                ReportService reportService = new ReportService()
                {
                    OutPath =  $"{rootPath}{dataFilePath}{docName}" ,// rootPath + dataFilePath + docName,
                    StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                    HtmlContent = htmlView,
                    Orientation = DinkToPdf.Orientation.Portrait
                  
                };
                request.Converter.Convert(reportService.PdfDocument);
                string printPdfPath = $"{dataFilePath}{docName}";
                return printPdfPath;

            }
            catch(Exception ex)
            {
                throw new  ApplicationException(ex.ToString());
            }
        }
        #endregion
    }
}
