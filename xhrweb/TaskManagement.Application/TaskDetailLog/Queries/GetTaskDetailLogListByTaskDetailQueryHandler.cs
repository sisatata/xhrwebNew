using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetailLog.Queries.Models;

namespace TaskManagement.Application.TaskDetailLog.Queries
{
    public class GetTaskDetailLogListByTaskDetailQueryHandler : IRequestHandler<Queries.GetTaskDetailLogListByTaskDetail, List<TaskDetailLogVM>>
    {
        #region ctor
        public GetTaskDetailLogListByTaskDetailQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        #endregion
        #region properties
        private readonly DbConnection _connection;
        #endregion
        #region methods
        public async Task<List<TaskDetailLogVM>> Handle(Queries.GetTaskDetailLogListByTaskDetail request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from task. GetTaskDetailLogByTaskDetail ('{request.TaskDetailId}')"; ;

            var data = await _connection.QueryAsync<TaskDetailLogVM>(query);
            return data.ToList();
        }
        #endregion

    }
}
