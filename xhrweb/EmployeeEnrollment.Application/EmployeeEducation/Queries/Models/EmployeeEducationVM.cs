using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeEducation.Queries.Models
{
    public class EmployeeEducationVM
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid? EducationalDegreeId { get; set; }
        public Guid? EducationalInstituteId { get; set; }
        public string EducationalDegreeName { get; set; }
        public string EducationalInstituteName { get; set; }
        public string Session { get; set; }
        public string PassingYear { get; set; }
        public string BoardorUniversity { get; set; }
        public string Result { get; set; }
        public string ResultType { get; set; }
        public decimal? OutOf { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid CompanyId { get; set; }
    }
}
