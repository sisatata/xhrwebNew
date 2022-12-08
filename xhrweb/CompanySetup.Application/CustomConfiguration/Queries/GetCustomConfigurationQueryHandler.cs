using CompanySetup.Application.CustomConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.CustomConfiguration.Queries
{
    public class GetCustomConfigurationQueryHandler : IRequestHandler<Queries.GetCustomConfiguration, CustomConfigurationVM>
    {
        public GetCustomConfigurationQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<CustomConfigurationVM>
            Handle(Queries.GetCustomConfiguration request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetCustomConfigurationById ({request.Id})";

            var data = await _connection.QueryFirstAsync<CustomConfigurationVM>
                (query);
            return data;
        }
    }
}

