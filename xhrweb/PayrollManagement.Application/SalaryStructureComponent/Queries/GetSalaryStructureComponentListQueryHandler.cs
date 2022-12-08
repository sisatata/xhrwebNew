using PayrollManagement.Application.SalaryStructureComponent.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.SalaryStructureComponent.Queries
{
    public class GetSalaryStructureComponentListQueryHandler : IRequestHandler<Queries.GetSalaryStructureComponentList, List<SalaryStructureComponentVM>>
    {
        public GetSalaryStructureComponentListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<SalaryStructureComponentVM>> Handle(Queries.GetSalaryStructureComponentList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetSalaryStructureComponentByStructureId ('{request.StructureId}')"; ;

            var data = await _connection.QueryAsync<SalaryStructureComponentVM>(query);
            return data.ToList();
        }
    }
}
