using PayrollManagement.Application.IncomeTaxPayerType.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.IncomeTaxPayerType.Queries
{
    public class GetIncomeTaxPayerTypeListQueryHandler : IRequestHandler<Queries.GetIncomeTaxPayerTypeList, List<IncomeTaxPayerTypeVM>>
    {
        public GetIncomeTaxPayerTypeListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<IncomeTaxPayerTypeVM>> Handle(Queries.GetIncomeTaxPayerTypeList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetIncomeTaxPayerTypeByCompany('{request.CompanyId}')";

            var data = await _connection.QueryAsync<IncomeTaxPayerTypeVM>(query);
            return data.ToList();
        }
    }
}
