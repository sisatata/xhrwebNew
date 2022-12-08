using PayrollManagement.Application.BankBranch.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BankBranch.Queries
{
    public static class Queries
    {
        public class GetBankBranchList : IRequest<List<BankBranchVM>>
        {
            public Guid CompanyId { get; set; }
        }
        public class GetBankBranchListByBank : IRequest<List<BankBranchVM>>
        {
            public Guid BankId { get; set; }
        }

        public class GetBankBranch : IRequest<BankBranchVM>
        {
            public Guid Id { get; set; }
        }
    }
}
