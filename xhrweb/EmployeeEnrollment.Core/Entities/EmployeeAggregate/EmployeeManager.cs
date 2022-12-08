using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeManager : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {

        public Guid EmployeeId { get; private set; }
        public Guid? ManagerId { get; private set; }
        public Boolean IsPrimaryManager { get; private set; }
        public Guid? AssignedBy { get; private set; }
        public DateTime? AssignDate { get; private set; }
        public Guid? UnAssignedBy { get; private set; }
        public DateTime? UnAssignDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid? ManagerTypeId { get; private set; }


        public EmployeeManager(Guid id) : base(id) { }
        private EmployeeManager() : base(Guid.NewGuid()) { }

        public static EmployeeManager Create(

         Guid employeeId,
         Guid? managerId,
         Boolean isPrimaryManager,
         Guid? assignedBy,
         DateTime? assignDate,
         //Guid? unAssignedBy,
         //DateTime? unAssignDate,
         //Boolean isDeleted,
         Guid companyId,
         Guid? managerTypeId

        )

        {
            var oModel = new EmployeeManager(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.ManagerId = managerId;
            oModel.IsPrimaryManager = isPrimaryManager;
            oModel.AssignedBy = assignedBy;
            oModel.AssignDate = assignDate;
            //oModel.UnAssignedBy = unAssignedBy;
            //oModel.UnAssignDate = unAssignDate;
            oModel.IsDeleted = false;
            oModel.CompanyId = companyId;
            oModel.ManagerTypeId = managerTypeId;

            return oModel;

        }


        public void UpdateEmployeeManager
            (

         Guid employeeId,
         Guid? managerId,
         Boolean isPrimaryManager,
         Guid? assignedBy,
         DateTime? assignDate,
         //Guid? unAssignedBy,
         //DateTime? unAssignDate,
         //Boolean isDeleted,
         Guid companyId,
         Guid? managerTypeId

        )
        {
            EmployeeId = employeeId;
            ManagerId = managerId;
            IsPrimaryManager = isPrimaryManager;
            AssignedBy = assignedBy;
            AssignDate = assignDate;
            //UnAssignedBy = unAssignedBy;
            //UnAssignDate = unAssignDate;
            //IsDeleted = isDeleted;
            CompanyId = companyId;
            ManagerTypeId = managerTypeId;
        }


        public void MarkAsDeleteEmployeeManager(Guid unAssignedBy)
        {
            IsDeleted = true;
            UnAssignedBy = unAssignedBy;
            UnAssignDate = DateTime.Now;
        }


    }
}

