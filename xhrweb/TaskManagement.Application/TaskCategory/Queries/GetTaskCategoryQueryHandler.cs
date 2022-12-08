using TaskManagement.Application.TaskCategory.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace TaskManagement.Application.TaskCategory.Queries
{
    public class GetTaskCategoryQueryHandler : IRequestHandler<Queries.GetTaskCategory, TaskCategoryVM>
    {
        public GetTaskCategoryQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<TaskCategoryVM>
            Handle(Queries.GetTaskCategory request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task. GetTaskCategoryById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<TaskCategoryVM>
                (query);
            return data;
        }
    }
}

