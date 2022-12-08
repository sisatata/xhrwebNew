using PayrollManagement.Application.EmployeePaidIncomeTax.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeePaidIncomeTax.Queries
{
    public class GetEmployeePaidIncomeTaxListQueryHandler : IRequestHandler<Queries.GetEmployeePaidIncomeTaxList, List<EmployeePaidIncomeTaxVM>>
    {
        public GetEmployeePaidIncomeTaxListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeePaidIncomeTaxVM>> Handle(Queries.GetEmployeePaidIncomeTaxList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetEmployeePaidIncomeTaxById ('{request.CompanyId}')";

            var data = await _connection.QueryAsync<EmployeePaidIncomeTaxVM>(query);
            return data.ToList();
        }
    }
}
