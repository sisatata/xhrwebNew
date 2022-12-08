using System;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetail.Commands;
using TaskManagement.Application.TaskDetail.Queries;
using Microsoft.AspNetCore.Mvc;
using ASL.Hrms.Api.Reports;
using Asl.Infrastructure.ExcelReports;
using System.IO;

namespace ASL.Hrms.Api.Controllers
{
    public class TaskDetailController : BaseController
    {
        public TaskDetailController(IExcelReportService excelReportService)
        {
            _excelReportService = excelReportService;
        }
        private readonly IExcelReportService _excelReportService;
        #region Queries

        [HttpGet("company/{companyId}/employeeId/{employeeId}/userId/{userId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId, Guid? employeeId, Guid? userId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailList { CompanyId = companyId,EmployeeId = employeeId, UserId = userId });
            return Ok(data);
        }
        [HttpGet("company/{companyId}/parentTaskId/{parentTaskId}/employeeId/{employeeId}/userId/{userId}")]
        public async Task<IActionResult> GetByCompanyAndParent(Guid companyId, Guid? parentTaskId, Guid? employeeId, Guid? userId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailList { CompanyId = companyId, ParentTaskId= parentTaskId, EmployeeId= employeeId, UserId= userId });
            return Ok(data);
        }


        [HttpGet("userId/{userId}/employeeId/{employeeId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetTaskByEmployee(Guid employeeId, DateTime? startDate, DateTime? endDate, Guid? userId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailList { EmployeeId = employeeId, StartDate = startDate, EndDate = endDate, UserId= userId });
            return Ok(data);
        }
        [HttpGet("userId/{userId}/managerId/{managerId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetTaskByManager(Guid managerId, DateTime? startDate, DateTime? endDate, Guid? userId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailList { EmployeeId = managerId, StartDate = startDate, EndDate = endDate, UserId= userId });
            return Ok(data);
        }
        [HttpGet("parentTaskId/{parentTaskId}")]
        public async Task<IActionResult> GetTaskByParent(Guid parentTaskId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailList { ParentTaskId = parentTaskId });
            return Ok(data);
        }


        [HttpGet("companyId/{companyId}")]
        public async Task<IActionResult> GetByCompanyId(Guid companyId)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetailList { CompanyId = companyId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetTaskDetail { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateTaskDetail command)
        {
           
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateTaskDetail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteTaskDetail command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("taskDetailList-excel")]

        public async Task<IActionResult> TaskDetailListInExcel([FromBody] Commands.V1.TaskDetailListExcel command)
        {
            var data = await Mediator.Send(command);
            string excelName = $"TaskList.xlsx";
            var report = await _excelReportService.Export(data);
            return ReturnExcelFileFromFilePath(report, excelName);
            return Ok(data);
        }
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

        #endregion
    }
}
