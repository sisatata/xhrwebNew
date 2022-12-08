using System;


namespace EmployeeEnrollment.Application.EmployeeExperience.Commands
{
    public class EmployeeExperienceCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
