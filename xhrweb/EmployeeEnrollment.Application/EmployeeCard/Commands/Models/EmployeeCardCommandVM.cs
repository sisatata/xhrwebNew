using System;


namespace EmployeeEnrollment.Application.EmployeeCard.Commands
{
    public class EmployeeCardCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
