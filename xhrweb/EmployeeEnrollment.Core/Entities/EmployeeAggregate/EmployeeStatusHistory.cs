using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeStatusHistory : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid EmployeeId { get; private set; }
        public Int16 StatusId { get; private set; }
        public DateTime? ChangedDate { get; private set; }
        public string Remarks { get; private set; }
        public bool IsDeleted { get; private set; }

        public EmployeeStatusHistory(Guid id) : base(id) { }
        private EmployeeStatusHistory() : base(Guid.NewGuid()) { }

        public static EmployeeStatusHistory Create(
         Guid employeeId,
         Int16 statusId,
         DateTime? changedDate,
         string remarks

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");
            //Guard.Against.NullOrEmptyGuid(statusId, "StatusId");
            Guard.Against.OutOfSQLDateRange(changedDate.Value, "ChangedDate");
            Guard.Against.NullOrWhiteSpace(remarks, "Remarks");

            var oModel = new EmployeeStatusHistory(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.StatusId = statusId;
            oModel.ChangedDate = changedDate;
            oModel.Remarks = remarks;

            return oModel;

        }


        public void UpdateEmployeeStatusHistory
            (

         Guid employeeId,
         Int16 statusId,
         DateTime? changedDate,
         string remarks

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId, "EmployeeId");
            //Guard.Against.NullOrEmptyGuid(statusId, "StatusId");
            Guard.Against.OutOfSQLDateRange(changedDate.Value, "ChangedDate");
            Guard.Against.NullOrWhiteSpace(remarks, "Remarks");

            EmployeeId = employeeId;
            StatusId = statusId;
            ChangedDate = changedDate;
            Remarks = remarks;
        }


        public void MarkAsDeleteEmployeeStatusHistory()
        {
            IsDeleted = true;
        }


    }
}

