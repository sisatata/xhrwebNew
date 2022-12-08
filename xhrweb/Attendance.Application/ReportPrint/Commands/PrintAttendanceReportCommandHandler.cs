using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using ASL.Hrms.SharedKernel.Services;
using ASL.Utility.ExtensionMethods;
using Attendance.Application.AttendanceProcessedData.Queries.Models;
using Dapper;
using DinkToPdf;
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
    public class PrintAttendanceReportCommandHandler : IRequestHandler<PrintAttendanceReportCommand, string>
    {
        public PrintAttendanceReportCommandHandler(IConfiguration configuration, DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _configuration = configuration;
            _uriComposer = uriComposer;
            //_dataContext = dataContext;
        }
        private readonly DbConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly IUriComposer _uriComposer;
        //private readonly ApplicationDataContext _dataContext;
        public async Task<string> Handle(PrintAttendanceReportCommand request, CancellationToken cancellationToken)
        {
            var rootPath = _configuration.GetValue<string>("contentRoot:rootPath");
            var dataFilePath = _configuration.GetValue<string>("contentRoot:DataFile");
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = "";
            query = $"SELECT * from attendance.RPTGetAttendanceProcessedDataListByCompanyAndDateRange('{request.CompanyId}',null,'{startDate}','{endDate}')";
            var data = await _connection.QueryAsync<ReportAttendanceProcessedDataPdfVM>(query);
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

            query = $"SELECT * from main.getcompanylogouri('{request.CompanyId}')";
            var companyLogoUri = await _connection.QueryAsync<CompanyLogoUriVM>(query);
            var oData = data.OrderBy(x => x.Department).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName).ToList();
            var companyLogoUriData = companyLogoUri?.FirstOrDefault();
            var companyQuery = $"SELECT * from main.GetCompanyNameAndAddressByCompanyId('{request.CompanyId}')";
            var companyData = await _connection.QueryAsync<CompanyNameAndAddressVM>(companyQuery);
            if (!string.IsNullOrEmpty(companyLogoUriData.CompanyLogoUri) && oData.Count > 0)
            {
                var now = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
                oData.FirstOrDefault().CompanyLogoUri = _uriComposer.ComposeCompanyLogoUri(companyLogoUriData.CompanyLogoUri.ToString());
            }
            var dataList = oData.GroupBy(x => x.EmployeeId).ToList();

            // calculate total OT
            foreach (var item in dataList)
            {
                TimeSpan ts = new TimeSpan();
                foreach (var childItem in item)
                {
                    var timeSpan = (childItem.OTHour.ToString("HH:mm"));
                    TimeSpan time = TimeSpan.Parse(timeSpan);
                    ts = ts.Add(time);

                }
                // convert in hour
                var minutes = (double)ts.Minutes / 60.0;
                item.FirstOrDefault().TotalOT = $"{ts.Hours}{minutes:.00}";

            }

            var docName = $"Attendance_Report_{DateTime.Now.ConvertToUnixEpochInSecond()}.pdf";// DateTime.Now.ToString("yyyyMMdd_HHmmss")
            if (companyData != null && companyData.FirstOrDefault() != null && oData.Count() > 0)
            {
                oData[0].CompanyAddress = companyData.FirstOrDefault().CompanyAddress;
            }
            oData.OrderBy(x => x.Branch).ThenBy(x => x.Department).ThenBy(x => x.DesignationOrder).ThenBy(x => x.FullName).ToList();
            var htmlView = await request.Engine.RenderViewToStringAsync("Views/PrintMonthlyAttendanceReport.cshtml", oData);

            ReportService reportService = new ReportService()
            {
                OutPath = rootPath + dataFilePath + docName,
                StylePath = Path.Combine(Directory.GetCurrentDirectory(), "utility\\assets\\css\\", "reportstyles.css"),
                HtmlContent = htmlView

            };

            request.Converter.Convert(reportService.PdfDocument);

            string printPdfPath = $"{dataFilePath}{docName}";
            return printPdfPath;
        }
    }
}
