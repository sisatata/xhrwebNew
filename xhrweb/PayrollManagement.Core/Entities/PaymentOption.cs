using ASL.Hrms.SharedKernel;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class PaymentOption : BaseEntity<Guid>, IAggregateRoot
    {
        public Int16? PaymentOptionId { get; private set; }
        public string OptionName { get; private set; }
        public string OptionCode { get; private set; }
        public Int16? SortOrder { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public PaymentOption(Guid id) : base(id) { }
        private PaymentOption() : base(Guid.NewGuid()) { }

        public static PaymentOption Create(

         Int16? paymentOptionId,
         string optionName,
         string optionCode,
         Int16? sortOrder,
         Boolean isDeleted

        )

        {
            var oModel = new PaymentOption(Guid.NewGuid());

            oModel.PaymentOptionId = paymentOptionId;
            oModel.OptionName = optionName;
            oModel.OptionCode = optionCode;
            oModel.SortOrder = sortOrder;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdatePaymentOption
            (

         Int16? paymentOptionId,
         string optionName,
         string optionCode,
         Int16? sortOrder,
         Boolean isDeleted

        )
        {
            PaymentOptionId = paymentOptionId;
            OptionName = optionName;
            OptionCode = optionCode;
            SortOrder = sortOrder;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeletePaymentOption()
        {
            IsDeleted = true;
        }


    }
}

