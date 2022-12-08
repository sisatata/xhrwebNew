using EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeCustomConfiguration.Queries
{
    public class GetEmployeeCustomConfigurationListQueryHandler : IRequestHandler<Queries.GetEmployeeCustomConfigurationList, List<EmployeeCustomConfigurationVM>>
    {
        public GetEmployeeCustomConfigurationListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeCustomConfigurationVM>> Handle(Queries.GetEmployeeCustomConfigurationList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeCustomConfigurationById ({request.EmployeeId})"; ;

            var data = await _connection.QueryAsync<EmployeeCustomConfigurationVM>(query);
            return data.ToList();
        }
    }
}
