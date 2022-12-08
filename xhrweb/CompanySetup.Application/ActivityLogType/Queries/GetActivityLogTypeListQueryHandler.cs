using CompanySetup.Application.ActivityLogType.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.ActivityLogType.Queries
{
    public class GetActivityLogTypeListQueryHandler : IRequestHandler<Queries.GetActivityLogTypeList, List<ActivityLogTypeVM>>
    {
        public GetActivityLogTypeListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<ActivityLogTypeVM>> Handle(Queries.GetActivityLogTypeList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetActivityLogTypeById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<ActivityLogTypeVM>(query);
            return data.ToList();
        }
    }
}
