using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeEducation.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeEducation : IRequest<EmployeeEducationCommandVM>
            {
                
                public Guid EmployeeId { get; set; }
                public Guid? EducationalDegreeId { get; set; }
                public Guid? EducationalInstituteId { get; set; }
                public string Session { get; set; }
                public string PassingYear { get; set; }
                public string BoardorUniversity { get; set; }
                public string Result { get; set; }
                public string ResultType { get; set; }
                public decimal? OutOf { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class UpdateEmployeeEducation : IRequest<EmployeeEducationCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public Guid? EducationalDegreeId { get; set; }
                public Guid? EducationalInstituteId { get; set; }
                public string Session { get; set; }
                public string PassingYear { get; set; }
                public string BoardorUniversity { get; set; }
                public string Result { get; set; }
                public string ResultType { get; set; }
                public decimal? OutOf { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class MarkAsDeleteEmployeeEducation : IRequest<EmployeeEducationCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
