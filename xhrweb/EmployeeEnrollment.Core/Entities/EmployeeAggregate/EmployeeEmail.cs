using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeEmail : BaseEntity<Guid>, IAggregateRoot
    {
        //public Guid Id { get; private set; }
        public Guid EmployeeId { get; private set; }
        public string EmailAddress { get; private set; }
        public bool IsPrimary { get; private set; }
        public bool IsDeleted { get; private set; }

        public EmployeeEmail(Guid id) : base(id) { }
        private EmployeeEmail() : base(Guid.NewGuid()) { }

        public static EmployeeEmail Create(

         Guid employeeId,
         string emailAddress,
         bool isPrimary

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(emailAddress, "EmailAddress");

            var oModel = new EmployeeEmail(Guid.NewGuid());
            oModel.EmployeeId = employeeId;
            oModel.EmailAddress = emailAddress;
            oModel.IsPrimary = isPrimary;

            return oModel;

        }


        public void UpdateEmployeeEmail
            (

         Guid employeeId,
         string emailAddress,
         bool isPrimary

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(emailAddress, "EmailAddress");
            EmployeeId = employeeId;
            EmailAddress = emailAddress;
            IsPrimary = isPrimary;
        }


        public void MarkAsDeleteEmployeeEmail()
        {
            IsDeleted = true;
        }


    }
}

