using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetail.Queries.Models;

namespace TaskManagement.Application.TaskDetail.Queries
{
    public class GetTaskDetaiListByCompanyHandler : IRequestHandler<Queries.GetTaskDetailByCompany, List<TaskDetailVM>>
    {
        public GetTaskDetaiListByCompanyHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;
        public async Task<List<TaskDetailVM>> Handle(Queries.GetTaskDetailByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task. GetTaskDetailByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<TaskDetailVM>(query);
            return data.ToList();
        }

       
    }
}
