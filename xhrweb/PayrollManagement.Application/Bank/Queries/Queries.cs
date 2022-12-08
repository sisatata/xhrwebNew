using PayrollManagement.Application.Bank.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.Bank.Queries
{
    public static class Queries
    {
        public class GetBankList : IRequest<List<BankVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetBank : IRequest<BankVM>
        {
            public Guid Id { get; set; }
        }
    }
}
