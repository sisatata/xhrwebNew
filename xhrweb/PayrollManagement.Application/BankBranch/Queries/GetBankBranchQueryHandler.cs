using PayrollManagement.Application.BankBranch.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.BankBranch.Queries
{
    public class GetBankBranchQueryHandler : IRequestHandler<Queries.GetBankBranch, BankBranchVM>
    {
        public GetBankBranchQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BankBranchVM>
            Handle(Queries.GetBankBranch request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetBankBranchById ({request.Id})";

            var data = await _connection.QueryFirstAsync<BankBranchVM>
                (query);
            return data;
        }
    }
}

