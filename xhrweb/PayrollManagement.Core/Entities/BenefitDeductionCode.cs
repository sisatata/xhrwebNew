using ASL.Hrms.SharedKernel;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class BenefitDeductionCode : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? CompanyId { get; private set; }
        public string BenifitDeductionCode { get; private set; }
        public string BenifitDeductionCodeName { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public BenefitDeductionCode(Guid id) : base(id) { }
        private BenefitDeductionCode() : base(Guid.NewGuid()) { }

        public static BenefitDeductionCode Create(

         Guid? companyId,
         string benifitDeductionCode,
         string benifitDeductionCodeName

        )

        {
            var oModel = new BenefitDeductionCode(Guid.NewGuid());

            oModel.CompanyId = companyId;
            oModel.BenifitDeductionCode = benifitDeductionCode;
            oModel.BenifitDeductionCodeName = benifitDeductionCodeName;

            return oModel;

        }


        public void UpdateBenefitDeductionCode
            (

         Guid? companyId,
         string benifitDeductionCode,
         string benifitDeductionCodeName

        )
        {
            CompanyId = companyId;
            BenifitDeductionCode = benifitDeductionCode;
            BenifitDeductionCodeName = benifitDeductionCodeName;
        }


        public void MarkAsDeleteBenefitDeductionCode()
        {
            IsDeleted = true;
        }


    }
}

