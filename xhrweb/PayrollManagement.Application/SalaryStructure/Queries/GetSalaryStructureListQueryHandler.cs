using PayrollManagement.Application.SalaryStructure.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.SalaryStructure.Queries
{
    public class GetSalaryStructureListQueryHandler : IRequestHandler<Queries.GetSalaryStructureList, List<SalaryStructureVM>>
    {
        public GetSalaryStructureListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<SalaryStructureVM>> Handle(Queries.GetSalaryStructureList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetSalaryStructureByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<SalaryStructureVM>(query);
            return data.ToList();
        }
    }
}
