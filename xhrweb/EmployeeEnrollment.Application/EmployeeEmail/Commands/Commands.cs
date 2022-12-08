using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeEmail.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeEmail : IRequest<EmployeeEmailCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public string EmailAddress { get; set; }
                public bool IsPrimary { get; set; }
            }

            public class UpdateEmployeeEmail : IRequest<EmployeeEmailCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public string EmailAddress { get; set; }
                public bool IsPrimary { get; set; }
            }

            public class MarkAsDeleteEmployeeEmail : IRequest<EmployeeEmailCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
