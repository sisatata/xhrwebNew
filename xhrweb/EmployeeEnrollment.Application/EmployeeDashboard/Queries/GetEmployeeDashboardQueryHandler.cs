using Dapper;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using EmployeeEnrollment.Application.EmployeeDashboard.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetail.Queries.Models;

namespace EmployeeEnrollment.Application.EmployeeDashboard.Queries
{
    public class GetAdminDashBoardQueryHandler : IRequestHandler<Queries.GetEmployeeDashboard, EmployeeDashboardVM>
    {
        public GetAdminDashBoardQueryHandler(DbConnection connection, IMediator mediator)
        {
            _connection = connection;
            _mediator = mediator;
        }
        private readonly DbConnection _connection;
        private readonly IMediator _mediator;

        public async Task<EmployeeDashboardVM> Handle(Queries.GetEmployeeDashboard request, CancellationToken cancellationToken)
        {
            EmployeeDashboardVM employeeDashboardVM = new EmployeeDashboardVM();

            var query = $"SELECT * from  employee.GetEmployeeDashboardAttendanceData('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<DailyAttendanceSummaryVM>(query);

            employeeDashboardVM.EmployeeAttendance = data;

            var queryLeaveData = $"SELECT * from employee.GetEmployeeDashboardLeaveData('{request.Id}')";

            DateTime now = DateTime.Now.Date;
            var startDate = new DateTime(now.Year, now.Month, 1);

            DateTime format = (DateTime)now;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)startDate;
            string sttDate = format.ToString("yyyy-MM-dd");

            var dataLeave = await _connection.QueryAsync<EmployeeLeaveVM>(queryLeaveData);
            employeeDashboardVM.EmployeeLeaves = dataLeave.ToList();

            var queryBillData = $"SELECT * from payroll.GetBenefitBillClaimSummaryByEmployee('{request.Id}','{sttDate}','{endDate}')";

            var dataBill = await _connection.QueryAsync<BenefitBillClaimSummaryVM>(queryBillData);
            employeeDashboardVM.BenefitBillClaimSummaries = dataBill.ToList();

            employeeDashboardVM.EmployeeId = request.Id;

            var taskDetailData = $"SELECT * from task.GetLatestTaskByEmployee('{request.Id}')";
            var taskDetail = await _connection.QueryAsync<TaskDetailVM>(taskDetailData);
            employeeDashboardVM.Tasks = taskDetail.ToList();

            NotificationSummaryVM oModel = new NotificationSummaryVM();

            var notificSum = $"SELECT * from main.GetNotificationByOwner('{request.Id}')"; ;

            var dataNotificSum = await _connection.QueryAsync<NotificationVM>(query);
            oModel.NotificationList = dataNotificSum.ToList();

            oModel.ReadCount = dataNotificSum == null ? (short)0 : (short)dataNotificSum.ToList().Count(x => x.IsViewed == true);
            oModel.UnReadCount = dataNotificSum == null ? (short)0 : (short)dataNotificSum.ToList().Count(x => x.IsViewed == false);

            var queryEmp = $"SELECT * from  employee.GetEmployeeById('{request.Id}')";

            var dataEmp = await _connection.QueryFirstAsync<EmployeeVM>(queryEmp);

            employeeDashboardVM.NotificationSummary = oModel;

            var officeNotice = $"SELECT * from main.GetOfficeNoticeActiveAndPublishedByCompany('{dataEmp?.CompanyId}')"; ;

            var dataOfficeNotice = await _connection.QueryAsync<OfficeNoticeVM>(query);
            employeeDashboardVM.OfficeNotices = dataOfficeNotice.ToList();

            return employeeDashboardVM;
        }

    }
}
