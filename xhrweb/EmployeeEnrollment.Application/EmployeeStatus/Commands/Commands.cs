using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeStatus.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeStatus : IRequest<EmployeeStatusCommandVM>
            {
                public string EmployeeStatusName { get; set; }
                public string EmployeeStatusNameLC { get; set; }
                public Int16? Rank { get; set; }
                public Guid CompanyId { get; set; }

                public Int16 StatusId { get; set; }
            }

            public class UpdateEmployeeStatus : IRequest<EmployeeStatusCommandVM>
            {
                public Guid Id { get; set; }
                public string EmployeeStatusName { get; set; }
                public string EmployeeStatusNameLC { get; set; }
                public Int16? Rank { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class MarkAsDeleteEmployeeStatus : IRequest<EmployeeStatusCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
