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
    public class GetCustomConfigurationListByCompanyQueryHandler : IRequestHandler<Queries.GetCustomConfigurationListByCompany, List<CustomConfigurationCompanyCodeVM>>
    {
        public GetCustomConfigurationListByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CustomConfigurationCompanyCodeVM>> Handle(Queries.GetCustomConfigurationListByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetConfigurationsByCompanyAndCode ('{request.CompanyId}', '{request.Code}')"; ;

            var data = await _connection.QueryAsync<CustomConfigurationCompanyCodeVM>(query);
            return data.ToList();
        }
    }
}
