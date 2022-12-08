using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.PaymentOption.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreatePaymentOption : IRequest<PaymentOptionCommandVM>
            {
                public Int16? PaymentOptionId { get; set; }
                public string OptionName { get; set; }
                public string OptionCode { get; set; }
                public Int16? SortOrder { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class UpdatePaymentOption : IRequest<PaymentOptionCommandVM>
            {
                public Guid Id { get; set; }
                public Int16? PaymentOptionId { get; set; }
                public string OptionName { get; set; }
                public string OptionCode { get; set; }
                public Int16? SortOrder { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeletePaymentOption : IRequest<PaymentOptionCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
