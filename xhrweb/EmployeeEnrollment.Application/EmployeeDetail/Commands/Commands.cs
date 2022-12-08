using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDetail.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeDetail : IRequest<EmployeeDetailCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public string FathersName { get; set; }
                public string MothersName { get; set; }
                public string SpouseName { get; set; }
                public Guid? MaritalStatusId { get; set; }
                public Guid? ReligionId { get; set; }
                public string NID { get; set; }
                public string BID { get; set; }
                public Guid? BloodGroupId { get; set; }
            }

            public class UpdateEmployeeDetail : IRequest<EmployeeDetailCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public string FathersName { get; set; }
                public string MothersName { get; set; }
                public string SpouseName { get; set; }
                public Guid? MaritalStatusId { get; set; }
                public Guid? ReligionId { get; set; }
                public string NID { get; set; }
                public string BID { get; set; }
                public Guid? BloodGroupId { get; set; }
            }

            public class MarkAsDeleteEmployeeDetail : IRequest<EmployeeDetailCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
