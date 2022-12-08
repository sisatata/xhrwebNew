using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class SalaryStructure : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string StructureName { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public SalaryStructure(Guid id) : base(id) { }
        private SalaryStructure() : base(Guid.NewGuid()) { }

        public static SalaryStructure Create(

         string structureName,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid? companyId
        )

        {
            var oModel = new SalaryStructure(Guid.NewGuid());

            oModel.StructureName = structureName;
            oModel.Description = description;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateSalaryStructure
            (

         string structureName,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid? companyId
        )
        {
            StructureName = structureName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteSalaryStructure()
        {
            IsDeleted = true;
        }


    }
}

