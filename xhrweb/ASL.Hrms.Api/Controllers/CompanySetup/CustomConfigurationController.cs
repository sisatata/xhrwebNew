using System;
using System.Threading.Tasks;
using CompanySetup.Application.CustomConfiguration.Commands;
using CompanySetup.Application.CustomConfiguration.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class CustomConfigurationController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}/code/{code}")]
        public async Task<IActionResult> GetByCompanyId(Guid companyId, string code)
        {
            var data = await Mediator.Send(new Queries.GetCustomConfigurationListByCompany { CompanyId = companyId, Code = code });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}/code/{code}")]
        public async Task<IActionResult> GetByEmployeeId(Guid employeeId, string code)
        {
            var data = await Mediator.Send(new Queries.GetCustomConfigurationListByEmployee { EmployeeId = employeeId, Code = code });
            return Ok(data);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetCustomConfiguration { Id = id });
        //    return Ok(data);
        //}

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Commands.V1.CreateCustomConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateCustomConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteCustomConfiguration command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
