using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeDevice.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeDevice : IRequest<EmployeeDeviceCommandVM>
            {
                public string AccessToken { get; set; }
                public string Location { get; set; }
                public string Device { get; set; }
                public string OperatingSystem { get; set; }
                public string OSVersion { get; set; }
                public Guid? UserId { get; set; }
                public Guid? EmployeeId { get; set; }
            }

            public class UpdateEmployeeDevice : IRequest<EmployeeDeviceCommandVM>
            {
                public Guid Id { get; set; }
                public string AccessToken { get; set; }
                public string Location { get; set; }
                public string Device { get; set; }
                public string OperatingSystem { get; set; }
                public string OSVersion { get; set; }
                public Guid? UserId { get; set; }
                public Guid? EmployeeId { get; set; }
            }

            public class MarkAsDeleteEmployeeDevice : IRequest<EmployeeDeviceCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
