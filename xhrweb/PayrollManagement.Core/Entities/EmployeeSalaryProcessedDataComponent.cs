using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeeSalaryProcessedDataComponent : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeSalaryProcessedDataId { get; private set; }
        public Guid? EmployeeSalaryComponentId { get; private set; }
        public Guid? BenefitDeductionId { get; private set; }
        public decimal? ComponentValue { get; private set; }
        public string Type { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        // not persisted
        public TrackingState State { get; set; }
        public EmployeeSalaryProcessedDataComponent(Guid id) : base(id) { }
        private EmployeeSalaryProcessedDataComponent() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeSalaryProcessedDataId,
         Guid? employeeSalaryComponentId,
         Guid? benefitDeductionId,
         decimal? componentValue,
         string type,
         Guid? companyId,
         Boolean isDeleted,
        string displayName,
        string description

        )

        {
            EmployeeSalaryProcessedDataId = employeeSalaryProcessedDataId;
            EmployeeSalaryComponentId = employeeSalaryComponentId;
            BenefitDeductionId = benefitDeductionId;
            ComponentValue = componentValue;
            Type = type;
            CompanyId = companyId;
            IsDeleted = isDeleted;
            State = TrackingState.Added;
            DisplayName = displayName;
            Description = description;
        }


        public void UpdateEmployeeSalaryProcessedDataComponent
            (

         Guid? employeeSalaryProcessedDataId,
         Guid? employeeSalaryComponentId,
         Guid? benefitDeductionId,
         decimal? componentValue,
         string type,
         Guid? companyId,
         Boolean isDeleted,
         string displayName,
         string description
        )
        {
            EmployeeSalaryProcessedDataId = employeeSalaryProcessedDataId;
            EmployeeSalaryComponentId = employeeSalaryComponentId;
            BenefitDeductionId = benefitDeductionId;
            ComponentValue = componentValue;
            Type = type;
            CompanyId = companyId;
            IsDeleted = isDeleted;
            State = TrackingState.Modified;
            DisplayName = displayName;
            Description = description;
        }


        public void MarkAsDeleteEmployeeSalaryProcessedDataComponent()
        {
            IsDeleted = true;
            State = TrackingState.Modified;
        }


    }
}

