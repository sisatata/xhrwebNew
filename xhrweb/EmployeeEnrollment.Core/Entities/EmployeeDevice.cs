using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeDevice : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string AccessToken { get; private set; }
        public string Location { get; private set; }
        public string Device { get; private set; }
        public string OperatingSystem { get; private set; }
        public string OSVersion { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public Boolean IsDeleted { get; private set; }

        // not persisted
        public TrackingState State { get; set; }
        public EmployeeDevice(Guid id) : base(id) { }
        private EmployeeDevice() : base(Guid.NewGuid()) { }

        public static EmployeeDevice Create(

         string accessToken,
         string location,
         string device,
         string operatingSystem,
         string oSVersion,
         Guid? userId,
         Guid? employeeId

        )

        {
            var oModel = new EmployeeDevice(Guid.NewGuid());

            oModel.AccessToken = accessToken;
            oModel.Location = location;
            oModel.Device = device;
            oModel.OperatingSystem = operatingSystem;
            oModel.OSVersion = oSVersion;
            oModel.UserId = userId;
            oModel.EmployeeId = employeeId;
            oModel.IsDeleted = false;
            return oModel;

        }


        public void UpdateEmployeeDevice
            (

         string accessToken,
         string location,
         string device,
         string operatingSystem,
         string oSVersion,
         Guid? userId,
         Guid? employeeId

        )
        {
            AccessToken = accessToken;
            Location = location;
            Device = device;
            OperatingSystem = operatingSystem;
            OSVersion = oSVersion;
            UserId = userId;
            EmployeeId = employeeId;
            IsDeleted = false;
        }


        public void MarkAsDeleteEmployeeDevice()
        {
            IsDeleted = true;
        }


    }
}

