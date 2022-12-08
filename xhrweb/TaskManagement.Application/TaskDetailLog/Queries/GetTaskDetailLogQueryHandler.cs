using TaskManagement.Application.TaskDetailLog.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace TaskManagement.Application.TaskDetailLog.Queries
{
    public class GetTaskDetailLogQueryHandler : IRequestHandler<Queries.GetTaskDetailLog, TaskDetailLogVM>
    {
        public GetTaskDetailLogQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<TaskDetailLogVM>
            Handle(Queries.GetTaskDetailLog request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task. GetTaskDetailLogById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<TaskDetailLogVM>
                (query);
            return data;
        }
    }
}

