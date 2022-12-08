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
    public class GetCustomConfigurationListQueryHandler : IRequestHandler<Queries.GetCustomConfigurationList, List<CustomConfigurationVM>>
    {
        public GetCustomConfigurationListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CustomConfigurationVM>> Handle(Queries.GetCustomConfigurationList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetCustomConfigurationById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<CustomConfigurationVM>(query);
            return data.ToList();
        }
    }
}
