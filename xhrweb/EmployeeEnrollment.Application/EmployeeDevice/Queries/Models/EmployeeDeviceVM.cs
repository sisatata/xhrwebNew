using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDevice.Queries.Models
{
    public class EmployeeDeviceVM
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public string Location { get; set; }
        public string Device { get; set; }
        public string OperatingSystem { get; set; }
        public string OSVersion { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
