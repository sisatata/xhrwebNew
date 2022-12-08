using System;
using System.Threading.Tasks;
using CompanySetup.Application.CommonLookUpType.Commands;
using CompanySetup.Application.CommonLookUpType.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class CommonLookUpTypeController : BaseController
    {
        #region Queries

        [HttpGet("companyId/{id}")]
        public async Task<IActionResult> GetByCompany(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetCommonLookUpTypeListByCompany { CompanyId = id });
            return Ok(data);
        }

        [HttpGet("parentCode/{parentCode}")]
        public async Task<IActionResult> GetByParentCode(string parentCode)
        {

            var data = await Mediator.Send(new Queries.GetCommonLookUpTypeListByParentCode { ParentCode = parentCode });
            return Ok(data);
        }

        [HttpGet("companyId/{companyId}/parentCode/{parentCode}")]
        public async Task<IActionResult> GetByCompanyAndParentCode(Guid companyId, string parentCode)
        {
            var data = await Mediator.Send(new Queries.GetCommonLookUpTypeListByParentCode { CompanyId = companyId, ParentCode = parentCode });
            return Ok(data);
        }

        [HttpGet("GetCommonSettingsByCompany/companyId/{companyId}/employeeId/{employeeId}")]
        public async Task<IActionResult> GetCommonSettingsByCompany(Guid companyId, Guid employeeId)
        {
            var data = await Mediator.Send(new Queries.GetCommonSettingsByCompany { CompanyId = companyId, EmployeeId = employeeId });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LookUpCommands.V1.CreateCommonLookUpType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LookUpCommands.V1.UpdateCommonLookUpType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] LookUpCommands.V1.MarkAsDeleteCommonLookUpType command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion
    }
}
