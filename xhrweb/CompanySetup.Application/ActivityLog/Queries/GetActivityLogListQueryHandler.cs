using CompanySetup.Application.ActivityLog.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.ActivityLog.Queries
{
    public class GetActivityLogListQueryHandler : IRequestHandler<Queries.GetActivityLogList, List<ActivityLogVM>>
    {
        public GetActivityLogListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<ActivityLogVM>> Handle(Queries.GetActivityLogList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetActivityLogById ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<ActivityLogVM>(query);
            return data.ToList();
        }
    }
}
