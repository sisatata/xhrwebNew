using TaskManagement.Application.TaskCategory.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManagement.Application.TaskCategory.Queries
{
    public class GetTaskCategoryListQueryHandler : IRequestHandler<Queries.GetTaskCategoryList, List<TaskCategoryVM>>
    {
        public GetTaskCategoryListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<TaskCategoryVM>> Handle(Queries.GetTaskCategoryList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task.GetTaskCategoryById ('{request.CompanyId}')"; 

            var data = await _connection.QueryAsync<TaskCategoryVM>(query);
            return data.ToList();
        }
    }
}
