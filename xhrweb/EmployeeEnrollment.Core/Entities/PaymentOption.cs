using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class PaymentOption : BaseEntity<Guid>
    {
        public Int16? PaymentOptionId { get; private set; }
        public string OptionName { get; private set; }
        public string OptionCode { get; private set; }
        public Int16? SortOrder { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public PaymentOption(Guid id) : base(id) { }
        private PaymentOption() : base(Guid.NewGuid()) { }

    }
}

