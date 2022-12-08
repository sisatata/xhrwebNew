using System;


namespace EmployeeEnrollment.Application.EmployeePhone.Commands
{
    public class EmployeePhoneCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
