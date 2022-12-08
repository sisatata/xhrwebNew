using System;
using System.Threading.Tasks;
using Attendance.Application.Attendance.Commands;
using Attendance.Application.Attendance.Queries;
using Attendance.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessCommands = Attendance.Application.AttendanceProcessedData.Commands;

namespace ASL.Hrms.Api.Controllers
{
    public class AttendanceController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        public AttendanceController(
            ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        #region Queries

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var data = await Mediator.Send(new Queries.GetAttendanceList());
        //    return Ok(data);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetAttendance { Id = id });
        //    return Ok(data);
        //}

        [HttpGet("GetAllClockInOutListByEmployeeAndDate/employeeId/{employeeId}/requestDate/{requestDate}")]
        public async Task<IActionResult> GetAllClockInOutListByEmployeeAndDate(Guid employeeId, DateTime requestDate)
        {
            var data = await Mediator.Send(new Queries.GetAllClockInOutListByEmployeeAndDate
            {
                EmployeeId = employeeId,
                RequestDate = requestDate
            });
            return Ok(data);
        }
        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateAttendance command)
        {
            command.AttendanceDate = DateTime.Now;
            var data = await Mediator.Send(command);

            //ProcessCommands.Commands.V1.AttendanceProcessDataCommand processingCommand = new ProcessCommands.Commands.V1.AttendanceProcessDataCommand
            //{
            //    EmployeeId = command.EmployeeId,
            //    ProcessingStartDate = command.AttendanceDate.Value.Date,
            //    ProcessingEndDate = command.AttendanceDate.Value.Date
            //};
            //var processingData = await Mediator.Send(processingCommand);

            return Ok(data);
        }

        [HttpPost("create-bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] Commands.V1.CreateAttendanceBulk command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("create-bulk-device")]
        public async Task<IActionResult> CreateBulkDevice([FromBody] Commands.V1.CreateAttendanceDeviceBulk command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return Ok(data);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateAttendance command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteAttendance command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
