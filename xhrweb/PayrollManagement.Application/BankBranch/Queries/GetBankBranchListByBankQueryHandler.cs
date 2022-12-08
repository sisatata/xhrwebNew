using PayrollManagement.Application.BankBranch.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BankBranch.Queries
{
    public class GetBankBranchListByBankQueryHandler : IRequestHandler<Queries.GetBankBranchListByBank, List<BankBranchVM>>
    {
        public GetBankBranchListByBankQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BankBranchVM>> Handle(Queries.GetBankBranchListByBank request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBankBranchByBankId('{request.BankId}')"; ;

            var data = await _connection.QueryAsync<BankBranchVM>(query);
            return data.ToList();
        }
    }
}
