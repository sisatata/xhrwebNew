using PayrollManagement.Application.Bank.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.Bank.Queries
{
    public class GetBankListQueryHandler : IRequestHandler<Queries.GetBankList, List<BankVM>>
    {
        public GetBankListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BankVM>> Handle(Queries.GetBankList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBankByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<BankVM>(query);
            return data.ToList();
        }
    }
}
