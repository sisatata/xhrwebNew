using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Application.ShiftAllocation.Commands;
using Attendance.Application.ShiftAllocation.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers.Attendance
{

    public class ShiftAllocationController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}/branch/{branchId}/financialYear/{financialYearId}/monthCycle/{monthCycleId}/department/{departmentId}/designation/{designationId}/employee/{employeeId}")]
        public async Task<IActionResult> GetByCompanyAndEmployee(Guid companyId, Guid branchId, Guid financialYearId, Guid monthCycleId, Guid? departmentId = null, Guid? designationId = null, Guid? employeeId = null)
        {
            var data = await Mediator.Send(new Queries.GetShiftAllocationByCompanyAndEmployee { CompanyId = companyId, BranchId = branchId, DepartmentId = departmentId, DesignationId = designationId, FinancialYearId = financialYearId, MonthCycleId = monthCycleId, EmployeeId = employeeId });
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await Mediator.Send(new Queries.GetShiftAllocationById { ShiftAllocationId = id });
            return Ok(data);
        }

        #endregion

        #region Commands

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ShiftAllocationCommands.V1.SetShiftAllocation command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("process-shift-allocation")]
        public async Task<IActionResult> ProcessShiftAllocation([FromBody] ShiftAllocationCommands.V1.ProcessShiftAllocationBulk command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ShiftAllocationCommands.V1.UpdateShiftAllocation command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ShiftAllocationCommands.V1.MarkShiftAllocationAsDelete command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }

        #endregion


    }
}