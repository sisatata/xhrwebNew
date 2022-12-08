using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeeSalaryComponent : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeSalaryId { get;  set; }
        public Guid? SalaryStructureComponentId { get; private set; }
        public decimal? ComponentAmount { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public EmployeeSalaryComponent(Guid id) : base(id) { }
        private EmployeeSalaryComponent() : base(Guid.NewGuid()) { }

        public static EmployeeSalaryComponent Create(

         Guid? salaryStructureComponentId,
         decimal? componentAmount,
         Guid? companyId,
         Boolean isDeleted

        )

        {
            var oModel = new EmployeeSalaryComponent(Guid.NewGuid());
            
            oModel.SalaryStructureComponentId = salaryStructureComponentId;
            oModel.ComponentAmount = componentAmount;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateEmployeeSalaryComponent
            (

         Guid? employeeSalaryId,
         Guid? salaryStructureComponentId,
         decimal? componentAmount,
         Guid? companyId,
         Boolean isDeleted

        )
        {
            EmployeeSalaryId = employeeSalaryId;
            SalaryStructureComponentId = salaryStructureComponentId;
            ComponentAmount = componentAmount;
            CompanyId = companyId;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteEmployeeSalaryComponent()
        {
            IsDeleted = true;
        }


    }
}

