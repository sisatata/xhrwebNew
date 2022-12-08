using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeStatus : BaseEntity<Guid>, IAggregateRoot
    {
        public string EmployeeStatusName { get; private set; }
        public string EmployeeStatusNameLC { get; private set; }
        public Int16? Rank { get; private set; }
        public Guid CompanyId { get; private set; }
        public bool IsDeleted { get; private set; }
        public Int16 StatusId { get; private set; }
        public EmployeeStatus(Guid id) : base(id) { }
        private EmployeeStatus() : base(Guid.NewGuid()) { }

        public static EmployeeStatus Create(


         string employeeStatusName,
         string employeeStatusNameLC,
         Int16? rank,
         Guid companyId,
        Int16 statusId
        )

        {
            Guard.Against.NullOrWhiteSpace(employeeStatusName, "EmployeeStatusName");

            var oModel = new EmployeeStatus(Guid.NewGuid());


            oModel.EmployeeStatusName = employeeStatusName;
            oModel.EmployeeStatusNameLC = employeeStatusNameLC;
            oModel.Rank = rank;
            oModel.CompanyId = companyId;
            oModel.StatusId = statusId;
            return oModel;

        }


        public void UpdateEmployeeStatus
            (

         string employeeStatusName,
         string employeeStatusNameLC,
         Int16? rank,
         Guid companyId
        )
        {
            Guard.Against.NullOrWhiteSpace(employeeStatusName, "EmployeeStatusName");
            EmployeeStatusName = employeeStatusName;
            EmployeeStatusNameLC = employeeStatusNameLC;
            Rank = rank;
            CompanyId = companyId;
        }


        public void MarkAsDeleteEmployeeStatus()
        {
            IsDeleted = true;
        }


    }
}

