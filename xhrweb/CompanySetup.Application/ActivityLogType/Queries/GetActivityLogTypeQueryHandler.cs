using CompanySetup.Application.ActivityLogType.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.ActivityLogType.Queries
{
    public class GetActivityLogTypeQueryHandler : IRequestHandler<Queries.GetActivityLogType, ActivityLogTypeVM>
    {
        public GetActivityLogTypeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<ActivityLogTypeVM>
            Handle(Queries.GetActivityLogType request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetActivityLogTypeById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<ActivityLogTypeVM>
                (query);
            return data;
        }
    }
}

