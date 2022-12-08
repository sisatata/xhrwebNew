using System;
using System.Threading.Tasks;
using CompanySetup.Application.Upazila.Commands;
using CompanySetup.Application.Upazila.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class UpazilaController : BaseController
    {
        #region Queries

        [HttpGet("districtId/id")]
        public async Task<IActionResult> GetByDistrict(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetUpazilaList { DistrictId = id });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetUpazila { Id = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateUpazila command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateUpazila command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteUpazila command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
