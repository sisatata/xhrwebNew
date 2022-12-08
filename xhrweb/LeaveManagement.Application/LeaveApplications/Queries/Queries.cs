using LeaveManagement.Application.LeaveApplications.Queries.Models;

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveApplications.Queries
{
    public static class Queries
    {
        public class GetLeaveApplicationByCompany : IRequest<List<LeaveApplicationVM>>
        {
            public Guid CompanyId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }
        public class GetLeaveApplicationByParameter : IRequest<List<LeaveApplicationVM>>
        {
            public Guid CompanyId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string EmployeeName { get; set; }
            
            public string LeaveTypeName { get; set; }
            public string ApprovalStatusText { get; set; }
        }

        public class GetLeaveApplicationByEmployee : IRequest<LeaveApplicationSummaryVM>
        {
            
            public Guid EmployeeId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class GetLeaveApplicationById : IRequest<LeaveApplicationVM>
        {
            public Guid LeaveApplicationId { get; set; }
        }
        public class GetPendingLeaveNotificationByManager : IRequest<List<LeaveNotificationVM>>
        {
            public Guid ManagerId { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class GetLeaveDataToSendEmail : IRequest<List<LeaveDataForEmailVM>>
        {

        }
        public class GetLeaveApplication: IRequest<List<LeaveApplicationVM>>
        {
            public Guid? CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
            public string LeaveCalendar { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public string SearchText { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public List<string> LeaveTypeName { get; set; }
            public List<string> ApprovalStatusText { get; set; }
        }

    }
}
