using PayrollManagement.Application.IncomeTaxSlab.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.IncomeTaxSlab.Queries
{
    public class GetIncomeTaxSlabListQueryHandler : IRequestHandler<Queries.GetIncomeTaxSlabList, List<IncomeTaxSlabVM>>
    {
        public GetIncomeTaxSlabListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<IncomeTaxSlabVM>> Handle(Queries.GetIncomeTaxSlabList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetIncomeTaxSlabByCompany('{request.CompanyId}')";

            var data = await _connection.QueryAsync<IncomeTaxSlabVM>(query);
            return data.ToList();
        }
    }
}
