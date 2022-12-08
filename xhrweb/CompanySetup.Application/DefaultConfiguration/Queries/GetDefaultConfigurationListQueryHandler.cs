using CompanySetup.Application.DefaultConfiguration.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.DefaultConfiguration.Queries
{
    public class GetDefaultConfigurationListQueryHandler : IRequestHandler<Queries.GetDefaultConfigurationList, List<DefaultConfigurationVM>>
    {
        public GetDefaultConfigurationListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<DefaultConfigurationVM>> Handle(Queries.GetDefaultConfigurationList request, CancellationToken cancellationToken)
        {
              var query = $"SELECT * from main. GetConfigurationsByCompany ('{request.CompanyId}')"; 

            var data = await _connection.QueryAsync<DefaultConfigurationVM>(query);
            return data.ToList();
        }
    }
}
