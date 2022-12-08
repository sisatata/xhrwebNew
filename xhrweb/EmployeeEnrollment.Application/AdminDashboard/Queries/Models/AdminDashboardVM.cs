using System;
using System.Collections.Generic;
using System.Text;
namespace EmployeeEnrollment.Application.AdminDashBoard.Queries.Models
{
    public class AdminDashBoardVM
    {
        public Guid ManagerId { get; set; }
        public DailyAttendanceSummaryVM DailyAttendanceSummary { get; set; }

        public List<EmployeeConfirmationChartVM> EmployeeConfirmationChartList { get; set; }

        public ChartBillVM ChartBillData { get; set; }
        public List<DepartmentSalaryVM> DepartmentSalaryList { get; set; }
        public ChartPayrollVM ChartPayrollData { get; set; }

        public ChartPayrollVM ChartPayrollYearlyData { get; set; }

    }
}
