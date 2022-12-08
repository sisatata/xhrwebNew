using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeePhone.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeePhone : IRequest<EmployeePhoneCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public string PhoneNumber { get; set; }
                public Guid? PhoneTypeId { get; set; }
            }

            public class UpdateEmployeePhone : IRequest<EmployeePhoneCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public string PhoneNumber { get; set; }
                public Guid? PhoneTypeId { get; set; }
            }

            public class MarkAsDeleteEmployeePhone : IRequest<EmployeePhoneCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
