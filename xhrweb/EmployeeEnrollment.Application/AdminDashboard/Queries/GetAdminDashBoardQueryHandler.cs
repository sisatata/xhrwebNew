using Dapper;
using EmployeeEnrollment.Application.AdminDashBoard.Queries.Models;
using EmployeeEnrollment.Core.Entities;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.AdminDashBoard.Queries
{
    public class GetAdminDashBoardQueryHandler : IRequestHandler<Queries.GetAdminDashBoard, AdminDashBoardVM>
    {
        public GetAdminDashBoardQueryHandler(DbConnection connection,
            IReferenceRepository<FinancialYearMonth, Guid> repositoryFinYear)
        {
            _connection = connection;
            _repositoryFinYear = repositoryFinYear;
        }
        private readonly DbConnection _connection;
        private readonly IReferenceRepository<FinancialYearMonth, Guid> _repositoryFinYear;

        public async Task<AdminDashBoardVM> Handle(Queries.GetAdminDashBoard request, CancellationToken cancellationToken)
        {
            var yearMonthQ = new FinancialYearMonthByCompanyCurrentFilterSpecificaion(request.CompanyId);
            var yearMonths = await _repositoryFinYear.ListAsync(yearMonthQ).ConfigureAwait(false);

            var currentMonth = yearMonths.FirstOrDefault(x => x.MonthStartDate <= DateTime.Now.Date && x.MonthEndDate >= DateTime.Now.Date);
            int? currentMonthNumber = currentMonth?.MonthNumber;
            int lastMonthNumber = currentMonthNumber.Value - 1;
            int lastYearNumber = currentMonth.YearNumber - 1;
          

            var lastMonth = yearMonths.FirstOrDefault(x => x.MonthNumber == lastMonthNumber);
            var lastYear = yearMonths.FirstOrDefault(x => x.YearNumber == lastYearNumber);

            AdminDashBoardVM adminDashBoardVM = new AdminDashBoardVM();
            adminDashBoardVM.ManagerId = request.ManagerId;
            DateTime format = (DateTime)request.PunchDate;
            string punchDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from  attendance.GetDailyAtendanceSummaryByManager('{request.CompanyId}','{request.ManagerId}', '{punchDate}')";
            var data = await _connection.QueryFirstOrDefaultAsync<DailyAttendanceSummaryVM>(query);
            adminDashBoardVM.DailyAttendanceSummary = data;

            //Employee Confirmation data

            var queryConfirmEmp = $"SELECT * from employee.ChartEmployeeConfirmationByCompany ('{request.CompanyId}','{DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd")}','{DateTime.Now.ToString("yyyy-MM-dd")}')";
            var dataConfirmEmp = await _connection.QueryAsync<EmployeeConfirmationChartVM>(queryConfirmEmp);
            adminDashBoardVM.EmployeeConfirmationChartList = dataConfirmEmp.ToList();

            //Chart Bill DATA
            var queryBill = $"SELECT * from payroll.chartgetbillamount('{request.CompanyId}','{DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd")}','{DateTime.Now.ToString("yyyy-MM-dd")}')";
            var dataBill = await _connection.QueryAsync<ChartBillVM>(queryBill);
            adminDashBoardVM.ChartBillData = dataBill.FirstOrDefault();

            //Salary data by department
            var queryDeptSalary = $"SELECT * from payroll.chartgetlastmonthdepartmentwisesalary('{request.CompanyId}','{lastMonth.FinancialYearName}','{lastMonth.MonthCycleName}')";
            var dataDeptSalary = await _connection.QueryAsync<DepartmentSalaryVM>(queryDeptSalary);
            adminDashBoardVM.DepartmentSalaryList = dataDeptSalary.ToList();

            //last month payroll data
            var queryLastMonthPayroll = $"SELECT * from payroll.ChartGetLastMonthPayroll('{request.CompanyId}','{lastMonth.FinancialYearName}','{lastMonth.MonthCycleName}')";
            var dataLastMonthPayroll = await _connection.QueryAsync<ChartPayrollVM>(queryLastMonthPayroll);
            adminDashBoardVM.ChartPayrollData = dataLastMonthPayroll.FirstOrDefault();

            //last year payroll data
            string lastYearId = lastYear == null ? "00000000-0000-0000-0000-000000000000" : lastYear.FinancialYearId.ToString();
            var queryYearlyPayroll = $"SELECT * from payroll.ChartGetYearlyPayroll('{request.CompanyId}','{lastYearId}')";
            var dataYearlyPayroll = await _connection.QueryAsync<ChartPayrollVM>(queryYearlyPayroll);
            adminDashBoardVM.ChartPayrollYearlyData = dataYearlyPayroll.FirstOrDefault();

            return adminDashBoardVM;
        }

    }
}
