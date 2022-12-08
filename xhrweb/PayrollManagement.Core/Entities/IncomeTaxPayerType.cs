using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class IncomeTaxPayerType : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string PayerTypeCode { get; private set; }
        public string PayerTypeName { get; private set; }
        public string Remarks { get; private set; }
        public Boolean IsActive { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public IncomeTaxPayerType(Guid id) : base(id) { }
        private IncomeTaxPayerType() : base(Guid.NewGuid()) { }

        public static IncomeTaxPayerType Create(

         string payerTypeCode,
         string payerTypeName,
         string remarks,
         Boolean isActive,
         Guid? companyId

        )

        {
            var oModel = new IncomeTaxPayerType(Guid.NewGuid());

            oModel.PayerTypeCode = payerTypeCode;
            oModel.PayerTypeName = payerTypeName;
            oModel.Remarks = remarks;
            oModel.IsActive = isActive;
            oModel.IsDeleted = false;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateIncomeTaxPayerType
            (

         
         string payerTypeCode,
         string payerTypeName,
         string remarks,
         Boolean isActive,
         Guid? companyId
    
        )
        {
            PayerTypeCode = payerTypeCode;
            PayerTypeName = payerTypeName;
            Remarks = remarks;
            IsActive = isActive;
            IsDeleted = false;
            CompanyId = companyId;
        }


        public void MarkAsDeleteIncomeTaxPayerType()
        {
            IsDeleted = true;
        }


    }
}

