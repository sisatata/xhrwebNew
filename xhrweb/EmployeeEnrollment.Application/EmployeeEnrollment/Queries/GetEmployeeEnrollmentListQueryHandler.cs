using EmployeeEnrollment.Application.EmployeeEnrollment.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Queries
{
    public class GetEmployeeEnrollmentListQueryHandler : IRequestHandler<Queries.GetEmployeeEnrollmentList, List<EmployeeEnrollmentVM>>
    {
        public GetEmployeeEnrollmentListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeEnrollmentVM>> Handle(Queries.GetEmployeeEnrollmentList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeEnrollmentById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<EmployeeEnrollmentVM>(query);
            return data.ToList();
        }
    }
}
