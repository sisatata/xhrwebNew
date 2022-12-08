using EmployeeEnrollment.Application.EmployeeDevice.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace EmployeeEnrollment.Application.EmployeeDevice.Queries
{
    public static class Queries
    {
        public class GetEmployeeDeviceList : IRequest<List<EmployeeDeviceVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetEmployeeDevice : IRequest<EmployeeDeviceVM>
        {
            public Guid Id { get; set; }
        }
        public class GetEmployeeDeviceByEmployee : IRequest<EmployeeDeviceDetailVM>
        {
            public Guid EmployeeId { get; set; }
        }
    }
}
