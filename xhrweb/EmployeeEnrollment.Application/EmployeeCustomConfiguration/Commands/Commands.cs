using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeCustomConfiguration : IRequest<EmployeeCustomConfigurationCommandVM>
            {
                public string Code { get; set; }
                public string CustomValue { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid EmployeeId { get; set; }
            }

            public class UpdateEmployeeCustomConfiguration : IRequest<EmployeeCustomConfigurationCommandVM>
            {
                public Guid Id { get; set; }
                public string Code { get; set; }
                public string CustomValue { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid EmployeeId { get; set; }
            }

            public class MarkAsDeleteEmployeeCustomConfiguration : IRequest<EmployeeCustomConfigurationCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
