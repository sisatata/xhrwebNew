using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeImage.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeImage : IRequest<EmployeeImageCommandVM>
            {
                public Guid EmployeeImageId { get; set; }
                public Guid? EmployeeId { get; set; }
                public Guid? FamilyMemberId { get; set; }
                public string Photo { get; set; }
                public Guid? PhotoId { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class UpdateEmployeeImage : IRequest<EmployeeImageCommandVM>
            {
                public Guid EmployeeImageId { get; set; }
                public Guid? EmployeeId { get; set; }
                public Guid? FamilyMemberId { get; set; }
                public string Photo { get; set; }
                public Guid? PhotoId { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeleteEmployeeImage : IRequest<EmployeeImageCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
