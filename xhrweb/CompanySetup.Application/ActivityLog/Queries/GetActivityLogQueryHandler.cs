using CompanySetup.Application.ActivityLog.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.ActivityLog.Queries
{
    public class GetActivityLogQueryHandler : IRequestHandler<Queries.GetActivityLog, ActivityLogVM>
    {
        public GetActivityLogQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<ActivityLogVM>
            Handle(Queries.GetActivityLog request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetActivityLogById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<ActivityLogVM>
                (query);
            return data;
        }
    }
}

