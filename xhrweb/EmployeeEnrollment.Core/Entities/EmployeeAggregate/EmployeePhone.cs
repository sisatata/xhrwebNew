using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeePhone : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? EmployeeId { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid? PhoneTypeId { get; private set; }
        public bool IsDeleted { get; private set; }

        public EmployeePhone(Guid id) : base(id) { }
        private EmployeePhone() : base(Guid.NewGuid()) { }

        public static EmployeePhone Create(
         Guid? employeeId,
         string phoneNumber,
         Guid? phoneTypeId

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(phoneNumber, "PhoneNumber");

            var oModel = new EmployeePhone(Guid.NewGuid());
            oModel.EmployeeId = employeeId;
            oModel.PhoneNumber = phoneNumber;
            oModel.PhoneTypeId = phoneTypeId;

            return oModel;

        }


        public void UpdateEmployeePhone (
         Guid? employeeId,
         string phoneNumber,
         Guid? phoneTypeId
        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(phoneNumber, "PhoneNumber");

            EmployeeId = employeeId;
            PhoneNumber = phoneNumber;
            PhoneTypeId = phoneTypeId;
        }


        public void MarkAsDeleteEmployeePhone()
        {
            IsDeleted = true;
        }


    }
}

