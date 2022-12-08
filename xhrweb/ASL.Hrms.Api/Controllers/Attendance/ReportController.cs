using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asl.Infrastructure.ExcelReports;
using ASL.Hrms.Api.Reports;
using Attendance.Application.AttendanceProcessedData.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASL.Hrms.Api.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : BaseController
    {
        private readonly IExcelReportService _excelReportService;
        //public ReporteController(IMediator mediatR, IConfiguration configuration, IExcelReportService excelReportService)
        //{
        //    _excelReportService = excelReportService;
        //}

        //[HttpPost("attendance-report")]
        //public async Task<IActionResult> Post([FromBody] Queries.GetAttendanceProcessedReportData filter)
        //{
        //    var data = await Mediator.Send(filter);
        //    var report = await _excelReportService.Export(data);
        //    string excelName = $"AttendanceReport.xlsx";

        //    return ReturnExcelFileFromFilePath(report, excelName);
        //}

        //private IActionResult ReturnExcelFileFromFilePath(ExcelResponse<string> excelFile, string fileName)
        //{
        //    var stream = new MemoryStream();

        //    using (FileStream fileStream = System.IO.File.OpenRead(excelFile.Data))
        //    {
        //        stream.SetLength(fileStream.Length);
        //        fileStream.Read(stream.GetBuffer(), 0, (int)fileStream.Length);
        //    }

        //    stream.Position = 0;

        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //}
    }
}
