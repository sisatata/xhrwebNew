using System;
using System.Threading.Tasks;
using Attendance.Application.AttendanceProcessedData.Commands;
using Attendance.Application.AttendanceProcessedData.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class AttendanceProcessedDataController : BaseController
    {
        #region Queries

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var data = await Mediator.Send(new Queries.GetAttendanceProcessedDataList());
        //    return Ok(data);
        //}

        [HttpGet("GetByCompanyAndDateRange/companyId/{companyId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetByCompanyAndDateRange(Guid companyId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceProcessedDataList
            {
                CompanyId = companyId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }
        [HttpPost("GetAttendanceByCompany")]
        public async Task<IActionResult> GetAttendanceProcessedByCompany([FromBody] Queries.GetEmployeeAttendanceData input)
        {
            var data = await Mediator.Send(input);
            return Ok(data);
        }

        [HttpGet("GetByEmployeeAndDateRange/employeeId/{employeeId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetByEmployeeAndDateRange(Guid employeeId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceProcessedDataListByEmployeeAndDateRange
            {
                EmployeeId = employeeId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }

        [HttpGet("GetAttendanceDataByEmployeeAndDateRange/employeeId/{employeeId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> GetAttendanceDataByEmployeeAndDateRange(Guid employeeId, DateTime startDate, DateTime endDate)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceDataListByEmployeeAndDateRange
            {
                EmployeeId = employeeId,
                StartTime = startDate,
                EndTime = endDate
            });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetAttendanceProcessedData { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Commands.V1.CreateAttendanceProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateAttendanceProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteAttendanceProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}


        /// [HttpPost("attendance-process")]
        [HttpPost("process-attendance")]
        public async Task<IActionResult> AttendanceProcess([FromBody] Commands.V1.AttendanceProcessDataCommand command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
