using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class SalaryStructureComponent : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? SalaryStrutureId { get; private set; }
        public string SalaryComponentName { get; private set; }
        public decimal? Value { get; private set; }
        public string ValueType { get; private set; }
        public string PercentOn { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }
        public short SortOrder { get; private set; }

        public SalaryStructureComponent(Guid id) : base(id) { }
        private SalaryStructureComponent() : base(Guid.NewGuid()) { }

        public static SalaryStructureComponent Create(

         Guid? salaryStrutureId,
         string salaryComponentName,
         decimal? value,
         string valueType,
         string percentOn,
         Boolean isDeleted,
         Guid? companyId,
         short sortOrder

        )

        {
            var oModel = new SalaryStructureComponent(Guid.NewGuid());

            oModel.SalaryStrutureId = salaryStrutureId;
            oModel.SalaryComponentName = salaryComponentName.Trim();
            oModel.Value = value;
            oModel.ValueType = valueType;
            oModel.PercentOn = percentOn;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;
            oModel.SortOrder = sortOrder;
            return oModel;

        }


        public void UpdateSalaryStructureComponent
            (

         Guid? salaryStrutureId,
         string salaryComponentName,
         decimal? value,
         string valueType,
         string percentOn,
         Boolean isDeleted,
         Guid? companyId,
         short sortOrder

        )
        {
            SalaryStrutureId = salaryStrutureId;
            SalaryComponentName = salaryComponentName.Trim();
            Value = value;
            ValueType = valueType;
            PercentOn = percentOn;
            IsDeleted = isDeleted;
            CompanyId = companyId;
            SortOrder = sortOrder;
        }


        public void MarkAsDeleteSalaryStructureComponent()
        {
            IsDeleted = true;
        }


    }
}

