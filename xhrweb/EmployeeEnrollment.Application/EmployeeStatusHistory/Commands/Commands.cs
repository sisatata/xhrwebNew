using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeStatusHistory : IRequest<EmployeeStatusHistoryCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public Int16 StatusId { get; set; }
                public DateTime? ChangedDate { get; set; }
                public string Remarks { get; set; }
            }

            public class UpdateEmployeeStatusHistory : IRequest<EmployeeStatusHistoryCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public Int16 StatusId { get; set; }
                public DateTime? ChangedDate { get; set; }
                public string Remarks { get; set; }
            }

            public class MarkAsDeleteEmployeeStatusHistory : IRequest<EmployeeStatusHistoryCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
