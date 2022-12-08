using System;


namespace EmployeeEnrollment.Application.EmployeeDevice.Commands
{
    public class EmployeeDeviceCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
