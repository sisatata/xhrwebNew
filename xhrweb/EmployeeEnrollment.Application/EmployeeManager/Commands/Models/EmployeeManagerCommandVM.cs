using System;


namespace EmployeeEnrollment.Application.EmployeeManager.Commands
{
    public class EmployeeManagerCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
