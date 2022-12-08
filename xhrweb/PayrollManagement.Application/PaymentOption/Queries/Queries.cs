using PayrollManagement.Application.PaymentOption.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.PaymentOption.Queries
{
    public static class Queries
    {
        public class GetPaymentOptionList : IRequest<List<PaymentOptionVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetPaymentOption : IRequest<PaymentOptionVM>
        {
            public Guid Id { get; set; }
        }
    }
}
