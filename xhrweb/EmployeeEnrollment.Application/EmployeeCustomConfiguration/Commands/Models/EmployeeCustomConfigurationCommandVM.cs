using System;


namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Commands
{
    public class EmployeeCustomConfigurationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
