using System;


namespace EmployeeEnrollment.Application.EmployeeStatusHistory.Commands
{
    public class EmployeeStatusHistoryCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
