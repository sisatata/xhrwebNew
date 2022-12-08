using TaskManagement.Application.TaskDetailLog.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Application.TaskDetailLog.Queries
{
    public class GetTaskDetailLogListQueryHandler : IRequestHandler<Queries.GetTaskDetailLogList, List<TaskDetailLogVM>>
    {
        public GetTaskDetailLogListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<TaskDetailLogVM>> Handle(Queries.GetTaskDetailLogList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task. GetTaskDetailLogById ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<TaskDetailLogVM>(query);
            return data.ToList();
        }
    }
}
