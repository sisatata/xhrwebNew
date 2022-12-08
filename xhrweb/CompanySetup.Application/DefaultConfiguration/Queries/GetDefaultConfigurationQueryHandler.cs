using CompanySetup.Application.DefaultConfiguration.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.DefaultConfiguration.Queries
{
    public class GetDefaultConfigurationQueryHandler : IRequestHandler<Queries.GetDefaultConfiguration, DefaultConfigurationVM>
    {
        public GetDefaultConfigurationQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<DefaultConfigurationVM>
            Handle(Queries.GetDefaultConfiguration request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetConfigurationsById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<DefaultConfigurationVM>
                (query);
            return data;
        }
    }
}

