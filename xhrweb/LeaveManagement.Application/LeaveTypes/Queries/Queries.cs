using LeaveManagement.Application.LeaveTypes.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Application.LeaveTypes.Queries
{
    public static class Queries
    {
        public class GetLeaveTypeByCompany : IRequest<List<LeaveTypeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetLeaveTypeByCompanyForComboBox : IRequest<List<LeaveType>>
        {
            public Guid CompanyId { get; set; }
        }
        public class GetLeaveTypeByEmployee : IRequest<List<LeaveTypeVM>>
        {
            public Guid EmployeeId { get; set; }
        }

        public class GetLeaveTypeById : IRequest<LeaveTypeVM>
        {
            public Guid LeaveTypeId { get; set; }
        }
    }
}
