using PayrollManagement.Application.IncomeTaxInvestment.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.IncomeTaxInvestment.Queries
{
    public class GetIncomeTaxInvestmentListQueryHandler : IRequestHandler<Queries.GetIncomeTaxInvestmentList, List<IncomeTaxInvestmentVM>>
    {
        public GetIncomeTaxInvestmentListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<IncomeTaxInvestmentVM>> Handle(Queries.GetIncomeTaxInvestmentList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetIncomeTaxInvestmentByCompany('{request.CompanyId}')"; 

            var data = await _connection.QueryAsync<IncomeTaxInvestmentVM>(query);
            return data.ToList();
        }
    }
}
