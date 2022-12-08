using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class Bank : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public string BankName { get; private set; }
        public string BankNameLC { get; private set; }
        public Int32? SortOrder { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Int16? PaymentOptionId { get; private set; }


        public Bank(Guid id) : base(id) { }
        private Bank() : base(Guid.NewGuid()) { }

        public static Bank Create(

         string bankName,
         string bankNameLC,
         Int32? sortOrder,
         Guid? companyId,
         Boolean isDeleted,
         Int16? paymentOptionId

        )

        {
            var oModel = new Bank(Guid.NewGuid());

            oModel.BankName = bankName;
            oModel.BankNameLC = bankNameLC;
            oModel.SortOrder = sortOrder;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = isDeleted;
            oModel.PaymentOptionId = paymentOptionId;

            return oModel;

        }


        public void UpdateBank
            (

         string bankName,
         string bankNameLC,
         Int32? sortOrder,
         Guid? companyId,
         Boolean isDeleted,
         Int16? paymentOptionId

        )
        {
            BankName = bankName;
            BankNameLC = bankNameLC;
            SortOrder = sortOrder;
            CompanyId = companyId;
            IsDeleted = isDeleted;
            PaymentOptionId = paymentOptionId;
        }


        public void MarkAsDeleteBank()
        {
            IsDeleted = true;
        }


    }
}

