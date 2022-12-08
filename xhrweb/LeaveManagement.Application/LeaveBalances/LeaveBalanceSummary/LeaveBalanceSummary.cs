using LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveBalances.LeaveBalanceSummary
{
  public static  class LeaveBalanceSummary
    {
        public class GetLeaveBalanceSummary : IRequest<List<LeaveBalanceSummaryVM>>
        {
            public Guid? CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
            public string LeaveCalendar { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public string SearchText { get; set; }
        }
        public class GetLeaveBalanceSummaryByEmployee : IRequest<List<LeaveBalanceSummaryVM>>
        {
            public Guid EmployeeId { get; set; }
            public string LeaveCalendar { get; set; }
        }
    }
}
