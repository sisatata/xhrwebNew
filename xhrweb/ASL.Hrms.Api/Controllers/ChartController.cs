using CompanySetup.Application.Chart.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Application.Chart.Queries;
using ConfirmationChart =  EmployeeEnrollment.Application.Chart.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ASL.Hrms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : BaseController
    {
        private readonly IMediator _mediatR;
        #region ctor
        public ChartController(IMediator mediatR)
        {
            _mediatR = mediatR;

        }
        #endregion
        #region Queries
        [HttpGet("payroll-chart/company/{companyId}/financialYearName/{financialYearName}/monthCycleName/{monthCycleName}")]
        public async Task<IActionResult> PayrollChart(Guid? companyId, Guid? monthCycleId, string financialYearName, string monthCycleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = new PayrollChart
            {
                CompanyId = companyId,
                MonthCycleId = monthCycleId,
                FinancialYearName = financialYearName,
                MonthCycleName = monthCycleName

            };
            var response = await _mediatR.Send(data);
            return Ok(response);
        }

        [HttpGet("bill-chart/company/{companyId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> BillAmountChart(Guid? companyId, DateTime? startDate, DateTime? endDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = new BillChart
            {
                CompanyId = companyId,
                StartDate = startDate,
                EndDate = endDate

            };
            var response = await _mediatR.Send(data);
            return Ok(response);
        }

        [HttpGet("yearly-payroll-chart/companyId/{companyId}/financialyearId/{financialYearId}")]
        public async Task<IActionResult> YearlypayrollChart(Guid? companyId, Guid? financialYearId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = new YearlyPayrollChart
            {
                CompanyId = companyId,
               FinancialYearId = financialYearId

            };
            var response = await _mediatR.Send(data);
            return Ok(response);
        }

        [HttpGet("current-year-new-employees/companyId/{companyId}/currentYear/{currentYear}")]
        public async Task<IActionResult> CurrentYearNewEmployees(Guid companyId, string currentYear)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var data = new CompanyChartQueries.GetCurrentYearJoinedEmployeesByCurrentYear
            {
              
                CompanyId = companyId,
                CurrentYear = currentYear

            };
            var response = await _mediatR.Send(data);
            return Ok(response);
        }


        [HttpGet("employee-confirmation/companyId/{companyId}/startDate/{startDate}/endDate/{endDate}")]
        public async Task<IActionResult> EmployeeConfirmationChart(Guid companyId, DateTime startDate, DateTime endDate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = new ConfirmationChart.Queries.EmployeeConfiramtionChart
            {
                StartDate = startDate,
                EndDate= endDate,
                CompanyId = companyId,
                

            };
            var response = await _mediatR.Send(data);
            return Ok(response);
        }


        [HttpGet("department-monthly-salary/companyId/{companyId}/financialYearName/{financialYearName}/monthCycleName/{monthCycleName}")]
        public async Task<IActionResult> DepartmentMonthlySalary(Guid? companyId, string financialYearName, string monthCycleName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = new Queries.DepartmentMonthlySalary
            {
                CompanyId = companyId,
                FinancialYearName = financialYearName,
                MonthCycleName = monthCycleName

            };
            var response = await _mediatR.Send(data);
            return Ok(response);
        }

        #endregion
    }
}
