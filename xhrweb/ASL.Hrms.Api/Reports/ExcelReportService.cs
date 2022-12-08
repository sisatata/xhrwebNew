using Asl.Infrastructure.ExcelReports;
using ASL.Utility.ExtensionMethods;
using ASL.Utility.FileManager.Interfaces;
using Attendance.Application.AttendanceProcessedData.Queries.Models;

using Microsoft.Extensions.Configuration;
using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetail.Queries.Models;
using static EmployeeEnrollment.Application.Employee.Commands.Commands.V1;
using static TaskManagement.Application.TaskDetail.Commands.Commands.V1;

namespace ASL.Hrms.Api.Reports
{
    public class ExcelReportService : IExcelReportService
    {
        private readonly IConfiguration _configuration;
        private readonly IExcelFileManager _excelFileManager;

        public ExcelReportService(IConfiguration configuration,
            IExcelFileManager excelFileManager)
        {
            _configuration = configuration;
            _excelFileManager = excelFileManager;
        }


        public async Task<ExcelResponse<string>> Export(List<ReportAttendanceProcessedDataVM> data)
        {
            var reportFolder = $"{_configuration.GetValue<string>("ContentRoot:RootPath")}" +
                $"{_configuration.GetValue<string>("ContentRoot:ReportsPath")}";

            var fileName = $"Attendance_Report_{DateTime.Now.ConvertToUnixEpochInSecond()}.xlsx";
            var downloadUrl = Path.Combine(reportFolder, fileName);
            var sheetName = "AttendanceReport";

            return ProcessExportToExcel(data.ToList(), sheetName, downloadUrl);
        }

        public async Task<ExcelResponse<string>> Export(List<ReportBenefitBillClaimVM> data)
        {
            var reportFolder = $"{_configuration.GetValue<string>("ContentRoot:RootPath")}" +
                $"{_configuration.GetValue<string>("ContentRoot:ReportsPath")}";

            var fileName = $"Bill_Claim_Report_{DateTime.Now.ConvertToUnixEpochInSecond()}.xlsx";
            var downloadUrl = Path.Combine(reportFolder, fileName);
            var sheetName = "BillClaimReport";

            return ProcessExportToExcel(data.ToList(), sheetName, downloadUrl);
        }

        public async Task<ExcelResponse<string>> Export(List<EmployeeExportVM> data)
        {
            var reportFolder = $"{_configuration.GetValue<string>("ContentRoot:RootPath")}" +
                $"{_configuration.GetValue<string>("ContentRoot:ReportsPath")}";

            var fileName = $"EmployeeList_{DateTime.Now.ConvertToUnixEpochInSecond()}.xlsx";
            var downloadUrl = Path.Combine(reportFolder, fileName);
            var sheetName = "EmployeeList";

            return ProcessExportToExcel(data.ToList(), sheetName, downloadUrl);
        }

        public async Task<ExcelResponse<string>> Export(List<TaskDetailListExportVM> data)
        {
            var reportFolder = $"{_configuration.GetValue<string>("ContentRoot:RootPath")}" +
                $"{_configuration.GetValue<string>("ContentRoot:ReportsPath")}";

            var fileName = $"TaskList_{DateTime.Now.ConvertToUnixEpochInSecond()}.xlsx";
            var downloadUrl = Path.Combine(reportFolder, fileName);
            var sheetName = "TaskList_";

            return ProcessExportToExcel(data.ToList(), sheetName, downloadUrl);
        }

        private ExcelResponse<string> ProcessExportToExcel<T>(List<T> data, string sheetName, string downloadUrl, List<string> excludeColumns = null)
           where T :ASL.Utility.FileManager.Interfaces.IExcelDataDynamicType
        {
            excludeColumns ??= new List<string>();

            excludeColumns.Add(nameof(ReportBenefitBillClaimVM.IsBoldRow));
            excludeColumns.Add(nameof(ReportBenefitBillClaimVM.WillRemoveColumn));
            excludeColumns.Add(nameof(ReportBenefitBillClaimVM.PaymentStatus));

            _excelFileManager.PrepareFile(data, downloadUrl, sheetName, excludeColumns);
            return ExcelResponse<string>.GetResult(0, "OK", downloadUrl);
        }
    }
}
