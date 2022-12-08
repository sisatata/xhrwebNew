using System;


namespace EmployeeEnrollment.Application.EmployeeDetail.Commands
{
    public class EmployeeDetailCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
