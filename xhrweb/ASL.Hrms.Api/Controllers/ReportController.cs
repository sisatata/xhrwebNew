using System.IO;
using System.Threading.Tasks;
using Asl.Infrastructure.ExcelReports;
using ASL.Hrms.Api.Reports;
using AttendanceProcessedData = Attendance.Application.AttendanceProcessedData.Queries;
using BillClaim = PayrollManagement.Application.BenefitBillClaim.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DinkToPdf.Contracts;
using Wkhtmltopdf.NetCore;
using AttendanceReportPdf = Attendance.Application.ReportPrint.Commands;
using PayrollReportPdf = PayrollManagement.Application.ReportPrint.Commands;
using EmployeeEnrollmentPdf = EmployeeEnrollment.Application.ReportPrint.Commands;
using Microsoft.AspNetCore.Authorization;
using PayrollManagement.Application.ReportPrint.Commands;
using LeaveManagement.Application.ReportPrint.Commands;
using Attendance.Application.ReportPrint.Commands;
using static PayrollManagement.Application.ReportPrint.Commands.Commands.V1;
using TaskManagement.Application.ReportPrint.Commands;

namespace ASL.Hrms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [EnableCors("AllowOrigin")]
    public class ReportController : BaseController
    {
        private readonly IExcelReportService _excelReportService;
        public IConverter _converter;
        public IRazorViewToStringRenderer _engine;
        private readonly IMediator _mediatR;
        public ReportController(IMediator mediatR, IConfiguration configuration, IExcelReportService excelReportService, IConverter converter
            ,
            IRazorViewToStringRenderer engine
            )
        {
            _excelReportService = excelReportService;
            _converter = converter;
            _engine = engine;
            _mediatR = mediatR;
        }

        #region Attendance Report

        [HttpPost("attendance-report")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] AttendanceProcessedData.Queries.GetAttendanceProcessedReportData filter)
        {
            var data = await Mediator.Send(filter);
            var report = await _excelReportService.Export(data);
            
            string excelName = $"AttendanceReport.xlsx";

            return ReturnExcelFileFromFilePath(report, excelName);
        }
        [HttpPost("attendance-report-pdf")]
        public async Task<IActionResult> AttendanceReportPdf([FromBody] AttendanceReportPdf.PrintAttendanceReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new AttendanceReportPdf.PrintAttendanceReportCommand
            {
                CompanyId = filter.CompanyId,
                StartTime = filter.StartTime,
                EndTime = filter.EndTime,
                Engine = _engine,
                Converter = _converter,
                EmployeeIds = filter.EmployeeIds,
                BranchIds = filter.BranchIds,
                DepartmentIds = filter.DepartmentIds,
                DesignationIds = filter.DesignationIds
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }

        [HttpPost("job-card-summary")]
        public async Task<IActionResult> JobCardSummary([FromBody] PrintJobCardSummaryReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintJobCardSummaryReportCommand
            {
                CompanyId = filter.CompanyId,
                BranchIds = filter.BranchIds,
                DepartmentIds = filter.DepartmentIds,
                DesignationIds = filter.DesignationIds,
                StartTime = filter.StartTime,
                EndTime = filter.EndTime,
                Engine = _engine,
                Converter = _converter,
                EmployeeIds = filter.EmployeeIds
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }

        [HttpPost("attendance-report-pdf-file")]
        public async Task<IActionResult> AttendanceReportPdfFile([FromBody] AttendanceProcessedData.Queries.GetAttendanceProcessedReportData filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new AttendanceReportPdf.PrintAttendanceReportCommand
            {
                CompanyId = filter.CompanyId,
                StartTime = filter.StartTime,
                EndTime = filter.EndTime,
                Engine = _engine,
                Converter = _converter
            };
            var response = await _mediatR.Send(requestModel);

            var printGiftUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printGiftUrl = printGiftUrl });
        }
        [HttpPost("salary-report-pdf")]
        public async Task<IActionResult> SalaryReportPdf([FromBody] PrintSalaryReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintSalaryReportCommand
            {
                CompanyId = filter.CompanyId,
                FinancialYearId = filter.FinancialYearId,
                MonthCycleId = filter.MonthCycleId,
                Engine = _engine,
                EmployeeId = filter.EmployeeId,
                Converter = _converter
                /*EmployeeId = filter.EmployeeId*/
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        [HttpPost("leave-report-pdf")]
        public async Task<IActionResult> LeaveReportPdf([FromBody] PrintLeaveApplicationReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintLeaveApplicationReportCommand
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Engine = _engine,
                EmployeeName = filter.EmployeeName,
                ApprovalStatusText = filter.ApprovalStatusText,
                LeaveTypeName = filter.LeaveTypeName,
                Converter = _converter
                /*EmployeeId = filter.EmployeeId*/
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }

        [HttpPost("payslip-report-pdf")]
        public async Task<IActionResult> PaySlipReportPdf([FromBody] PrintPaySlipReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintPaySlipReportCommand
            {
                CompanyId = filter.CompanyId,
                EmployeeId = filter.EmployeeId,
                FinancialYearId = filter.FinancialYearId,
                MonthCycleId = filter.MonthCycleId,
                Engine = _engine,
                Converter = _converter
               
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        [HttpPost("bonus-report-pdf")]
        public async Task<IActionResult> BonusReportPdf([FromBody] Commands.V1.PrintBonusReport filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new Commands.V1.PrintBonusReport
            {
                CompanyId = filter.CompanyId,
                BonusTypeId = filter.BonusTypeId,
                FinancialYearId = filter.FinancialYearId,
                Engine = _engine,
                EmployeeId = filter.EmployeeId,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        [HttpPost("ot-summary-report-pdf")]
        public async Task<IActionResult> OTSummaryReportPdf([FromBody] Commands.V1.PrintOTSummaryReport filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new Commands.V1.PrintOTSummaryReport
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Engine = _engine,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }

        [HttpPost("employee-enroll-report-pdf")]
        public async Task<IActionResult> EmployeeEnrollReportPdf([FromBody] EmployeeEnrollmentPdf.Commands.V1.PrintEmployeeEnrollmentReport filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new EmployeeEnrollmentPdf.Commands.V1.PrintEmployeeEnrollmentReport
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Type = filter.Type,
                Engine = _engine,
                EmployeeId = filter.EmployeeId,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }

        [HttpPost("employee-confirmation-report-pdf")]
        public async Task<IActionResult> EmployeeConfirmationReportPdf([FromBody] EmployeeEnrollmentPdf.Commands.V1.PrintEmployeeConfirmationReport filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new EmployeeEnrollmentPdf.Commands.V1.PrintEmployeeConfirmationReport
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Engine = _engine,
                EmployeeId = filter.EmployeeId,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }

        [HttpPost("task-detail-report-pdf")]
        public async Task<IActionResult> TaskDetailReportPdf([FromBody] PrintTaskDetailReportPrintCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintTaskDetailReportPrintCommand
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                Engine = _engine,
                UserId = filter.UserId,
                EmployeeId = filter.EmployeeId,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        [HttpPost("attendance-details-report-pdf")]
        // PrintLeaveApplicationDetailsReportCommand
        public async Task<IActionResult> EmployeeAttendanceDetailsReportPdf([FromBody] PrintAttendanceDetailsReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintAttendanceDetailsReportCommand
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                DepartmentIds = filter.DepartmentIds,
                BranchIds = filter.BranchIds,
                DesignationIds = filter.DesignationIds,
                Engine = _engine,
                Type = filter.Type,
                EmployeeIds = filter.EmployeeIds,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        [HttpPost("leave-details-report-pdf")]
        //  PrintProvidentFundReport
        public async Task<IActionResult> LeaveApplicationDetailsReportPdf([FromBody] PrintLeaveApplicationDetailsReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintLeaveApplicationDetailsReportCommand
            {
                CompanyId = filter.CompanyId,
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                DepartmentIds = filter.DepartmentIds,
                BranchIds = filter.BranchIds,
                DesignationIds = filter.DesignationIds,
                Engine = _engine,
                ApprovalStatusText = filter.ApprovalStatusText,
                EmployeeIds = filter.EmployeeIds,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl });
        }
        [HttpPost("provident-fund-report")]
        public async Task<IActionResult> LeaveApplicationDetailsReportPdf([FromBody] PrintProvidentFundReport filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintProvidentFundReport
            {
                CompanyId = filter.CompanyId,
                FinancialYearId = filter.FinancialYearId,
                MonthCycleId = filter.MonthCycleId,
                Engine = _engine,
                EmployeeId = filter.EmployeeId,
                Converter = _converter

            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl });
        }


        #endregion

        #region
        [HttpPost("bill-claim-report")]
        [AllowAnonymous]
        public async Task<IActionResult> BillClaimReport([FromBody] BillClaim.Queries.GetBenefitBillClaimReportDataByCompany filter)
        {
            var data = await Mediator.Send(filter);
            var report = await _excelReportService.Export(data);
            string excelName = $"BillClaimReport.xlsx";

            return ReturnExcelFileFromFilePath(report, excelName);
        }
        [HttpPost("bill-claim-report-pdf")]
        public async Task<IActionResult> BillClaimReport([FromBody] PayrollReportPdf.PrintDailyConveyanceReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PayrollReportPdf.PrintDailyConveyanceReportCommand
            {
                CompanyId = filter.CompanyId,
                EmployeeId = filter.EmployeeId,
                StartTime = filter.StartTime,
                EndTime = filter.EndTime,
                Engine = _engine,
                Converter = _converter
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        // PrintEarnLeaveEncashmentReport


        [HttpPost("earn-leave-encashment-report")]
        public async Task<IActionResult> BillClaimReport([FromBody] PrintEarnLeaveEncashmentReport filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintEarnLeaveEncashmentReport
            {
                CompanyId = filter.CompanyId,
                FinancialYearId = filter.FinancialYearId,
                MonthCycleId = filter.MonthCycleId,
                Engine = _engine,
                Converter = _converter
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }
        [HttpPost("attendance-report-summary-pdf")]
        public async Task<IActionResult> AttendanceSummaryReport([FromBody] PrintAttendanceSummaryReportCommand filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestModel = new PrintAttendanceSummaryReportCommand
            {
                CompanyId = filter.CompanyId,
                EmployeeIds = filter.EmployeeIds,    
                BranchIds = filter.BranchIds,
                DepartmentIds = filter.DepartmentIds,
                DesignationIds = filter.DesignationIds,
                StartTime = filter.StartTime,
                EndTime = filter.EndTime,
                Engine = _engine,
                Converter = _converter
            };
            var response = await _mediatR.Send(requestModel);

            var printPdfUrl = $"{Request.Scheme}://{Request.Host}/{response}";
            return Ok(new { printPdfUrl = printPdfUrl });
        }



        #endregion
        private IActionResult ReturnExcelFileFromFilePath(ExcelResponse<string> excelFile, string fileName)
        {
            var stream = new MemoryStream();

            using (FileStream fileStream = System.IO.File.OpenRead(excelFile.Data))
            {
                stream.SetLength(fileStream.Length);
                fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
            }

            stream.Position = 0;

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
