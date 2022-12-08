using CompanySetup.Application.CustomConfiguration.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.CustomConfiguration.Queries
{
    public class GetCustomConfigurationListByEmployeeQueryHandler : IRequestHandler<Queries.GetCustomConfigurationListByEmployee, List<EmployeeCustomConfigurationVM>>
    {
        public GetCustomConfigurationListByEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeCustomConfigurationVM>> Handle(Queries.GetCustomConfigurationListByEmployee request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetEmployeeCustomConfiguration('{request.EmployeeId}','{request.Code}')";

            var data = await _connection.QueryAsync<EmployeeCustomConfigurationVM>(query);
            return data.ToList();
        }
    }
}
