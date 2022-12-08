using Asl.Infrastructure.ExcelReports;
using Attendance.Application.AttendanceProcessedData.Queries.Models;
using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetail.Queries.Models;
using static EmployeeEnrollment.Application.Employee.Commands.Commands.V1;
using static TaskManagement.Application.TaskDetail.Commands.Commands.V1;

namespace ASL.Hrms.Api.Reports
{
    public interface IExcelReportService
    {
        Task<ExcelResponse<string>> Export(List<ReportAttendanceProcessedDataVM> input);
        Task<ExcelResponse<string>> Export(List<ReportBenefitBillClaimVM> input);

        Task<ExcelResponse<string>> Export(List<EmployeeExportVM> input);


        Task<ExcelResponse<string>> Export(List<TaskDetailListExportVM> input);
    }
}
