using System;


namespace PayrollManagement.Application.EmployeeLeaveEncashment.Commands
{
    public class EmployeeLeaveEncashmentCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
