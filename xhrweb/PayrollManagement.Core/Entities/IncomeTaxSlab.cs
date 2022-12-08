using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class IncomeTaxSlab : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string SlabName { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsRunningFlag { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }
        public decimal? SlabAmount { get; private set; }
        public decimal? TaxablePercent { get; private set; }
        //public Guid? IncomeTaxPayerTypeId { get; private set; }
        public string TaxPayerTypeCode { get; private set; }
        public Int32? SlabOrder { get; private set; }



        public IncomeTaxSlab(Guid id) : base(id) { }
        private IncomeTaxSlab() : base(Guid.NewGuid()) { }

        public static IncomeTaxSlab Create(

         string slabName,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isRunningFlag,
         Guid? companyId,
         decimal? slabAmount,
         decimal? taxablePercent,
         string taxPayerTypeCode,
         Int32? slabOrder

        )

        {
            var oModel = new IncomeTaxSlab(Guid.NewGuid());

            oModel.SlabName = slabName;
            oModel.Description = description;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsRunningFlag = isRunningFlag;
            oModel.CompanyId = companyId;
            oModel.SlabAmount = slabAmount;
            oModel.TaxablePercent = taxablePercent;
            oModel.TaxPayerTypeCode = taxPayerTypeCode;
            oModel.SlabOrder = slabOrder;

            return oModel;

        }


        public void UpdateIncomeTaxSlab
            (


         string slabName,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isRunningFlag,
         Guid? companyId,
         decimal? slabAmount,
         decimal? taxablePercent,
         string taxPayerTypeCode,
         Int32? slabOrder

        )
        {
            SlabName = slabName;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            IsRunningFlag = isRunningFlag;
            CompanyId = companyId;
            SlabAmount = slabAmount;
            TaxablePercent = taxablePercent;
            TaxPayerTypeCode = taxPayerTypeCode;
            SlabOrder = slabOrder;
        }


        public void MarkAsDeleteIncomeTaxSlab()
        {
            IsDeleted = true;
        }


    }
}

