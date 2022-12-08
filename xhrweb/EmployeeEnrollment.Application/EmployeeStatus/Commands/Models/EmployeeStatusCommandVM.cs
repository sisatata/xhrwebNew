using System;


namespace EmployeeEnrollment.Application.EmployeeStatus.Commands
{
    public class EmployeeStatusCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
