using System;
using System.Threading.Tasks;
using PayrollManagement.Application.EmployeeSalaryProcessedData.Commands;
using PayrollManagement.Application.EmployeeSalaryProcessedData.Queries;
using FinancialYearQuery = PayrollManagement.Application.FinancialYear.Queries;
using Microsoft.AspNetCore.Mvc;

namespace ASL.Hrms.Api.Controllers
{
    public class EmployeeSalaryProcessedDataController : BaseController
    {
        #region Queries

        [HttpGet("company/{companyId}/financialYear/{financialYearId}/monthCycle/{monthCycleId}")]
        public async Task<IActionResult> GetByCompany(Guid companyId, Guid financialYearId, Guid monthCycleId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryProcessedDataListByCompany
            {
                CompanyId = companyId,
                FinancialYearId = financialYearId,
                MonthCycleId = monthCycleId
            });
            return Ok(data);
        }

        [HttpGet("employee/{employeeId}/financialYear/{financialYearId}/monthCycle/{monthCycleId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId, Guid financialYearId, Guid monthCycleId)
        {
            var data = await Mediator.Send(new Queries.GetEmployeeSalaryProcessedDataListByEmployee
            {
                EmployeeId = employeeId,
                FinancialYearId = financialYearId,
                MonthCycleId = monthCycleId
            });
            return Ok(data);
        }

        [HttpGet("get-salary-year-month-by-employee/{employeeId}")]
        public async Task<IActionResult> GetSalaryYearMonthByEmployee(Guid employeeId)
        {
            var data = await Mediator.Send(new FinancialYearQuery.Queries.GetFinancialYearByEmployeeId
            {
                EmployeeId = employeeId
            });
            return Ok(data);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var data = await Mediator.Send(new Queries.GetEmployeeSalaryProcessedData { Id = id });
        //    return Ok(data);
        //}

        #endregion

        #region Commands

        [HttpPost("generate-salary")]
        public async Task<IActionResult> GenerateSalary([FromBody] Commands.V1.GenerateEmployeeSalaryProcessedData command)
        {
            var data = await Mediator.Send(command);
            return Ok(data);
        }
        [HttpPost("employee-salary-processed-data")]
        public async Task<IActionResult> GetEmployeeSalaryProcessedData([FromBody] Queries.GetEmployeeSalaryProcessedDataWithFilter input)
        {
            var data = await Mediator.Send(input);
            return Ok(data);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] Commands.V1.UpdateEmployeeSalaryProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromBody] Commands.V1.MarkAsDeleteEmployeeSalaryProcessedData command)
        //{
        //    var data = await Mediator.Send(command);
        //    return Ok(data);
        //}

        #endregion
    }
}
