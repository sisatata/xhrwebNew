using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeFamilyMember : IRequest<EmployeeFamilyMemberCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public string FamiliyMemberName { get; set; }
                public DateTime? DateOfBirth { get; set; }
                public string EducationClass { get; set; }
                public string EducationalInstitute { get; set; }
                public string EducationYear { get; set; }
                public Guid RelationTypeId { get; set; }
                public Boolean IsDependant { get; set; }
                public Boolean IsEligibleForMedical { get; set; }
                public Boolean IsEligibleForEducation { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class UpdateEmployeeFamilyMember : IRequest<EmployeeFamilyMemberCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public string FamiliyMemberName { get; set; }
                public DateTime? DateOfBirth { get; set; }
                public string EducationClass { get; set; }
                public string EducationalInstitute { get; set; }
                public string EducationYear { get; set; }
                public Guid RelationTypeId { get; set; }
                public Boolean IsDependant { get; set; }
                public Boolean IsEligibleForMedical { get; set; }
                public Boolean IsEligibleForEducation { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class MarkAsDeleteEmployeeFamilyMember : IRequest<EmployeeFamilyMemberCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
