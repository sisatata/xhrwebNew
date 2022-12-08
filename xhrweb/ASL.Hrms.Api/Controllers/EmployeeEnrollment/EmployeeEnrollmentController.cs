using System;
using System.Threading.Tasks;
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Application.EmployeeEnrollment.Commands;
using EmployeeEnrollment.Application.EmployeeEnrollment.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeEnrollmentController : BaseController
    {
        public EmployeeEnrollmentController(IOptions<PlanetHRSettings> settings)
        {
            _settings = settings;
        }
        private readonly IOptions<PlanetHRSettings> _settings;
        #region Queries

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeEnrollmentList());
        //    return Ok(data);
        //}

        [HttpGet("employeeId/{employeeId}")]
        public async Task<IActionResult> Get(Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeEnrollment { EmployeeId = employeeId });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateEmployeeEnrollment command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeEnrollment command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeEnrollment command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }



        [HttpPost("upload-image")]
        public async Task<IActionResult> Post([FromForm] Commands.V1.UpdateProfilePicture command)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files[0].Length > 0)
            {
                var employeeImage = Request.Form.Files?[0];

                var settings = _settings.Value;

                command.EmployeeImage = employeeImage;
                command.Settings = settings;

                var data = await Mediator.Send(command);
                return Ok(data);
            }

            return null;
        }
        [HttpPost("upload-signature")]
        public async Task<IActionResult> Post([FromForm] Commands.V1.UpdateSignature command)
        {
            if (Request.Form.Files != null && Request.Form.Files.Count > 0 && Request.Form.Files[0].Length > 0)
            {
                var employeeSignature = Request.Form.Files?[0];

                var settings = _settings.Value;

                command.EmployeeSignature = employeeSignature;
                command.Settings = settings;

                var data = await Mediator.Send(command);
                return Ok(data);
            }

            return null;
        }



        #endregion
    }
}
