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
    public class GetBankBranchListQueryHandler : IRequestHandler<Queries.GetBankBranchList, List<BankBranchVM>>
    {
        public GetBankBranchListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BankBranchVM>> Handle(Queries.GetBankBranchList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetBankBranchById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<BankBranchVM>(query);
            return data.ToList();
        }
    }
}
