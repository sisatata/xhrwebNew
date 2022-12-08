using TaskManagement.Application.TaskDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace TaskManagement.Application.TaskDetail.Queries
{
    public class GetTaskDetailQueryHandler : IRequestHandler<Queries.GetTaskDetail, TaskDetailVM>
    {
        public GetTaskDetailQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<TaskDetailVM>
            Handle(Queries.GetTaskDetail request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task. GetTaskDetailById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<TaskDetailVM>
                (query);
            return data;
        }
    }
}

