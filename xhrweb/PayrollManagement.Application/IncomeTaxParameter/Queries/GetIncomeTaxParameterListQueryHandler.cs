using PayrollManagement.Application.IncomeTaxParameter.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.IncomeTaxParameter.Queries
{
    public class GetIncomeTaxParameterListQueryHandler : IRequestHandler<Queries.GetIncomeTaxParameterList, List<IncomeTaxParameterVM>>
    {
        public GetIncomeTaxParameterListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<IncomeTaxParameterVM>> Handle(Queries.GetIncomeTaxParameterList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetIncomeTaxParameterByCompany('{request.CompanyId}')";

            var data = await _connection.QueryAsync<IncomeTaxParameterVM>(query);
            return data.ToList();
        }
    }
}
