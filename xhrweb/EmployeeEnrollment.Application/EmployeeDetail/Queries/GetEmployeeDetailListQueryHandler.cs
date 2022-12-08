using EmployeeEnrollment.Application.EmployeeDetail.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeDetail.Queries
{
    public class GetEmployeeDetailListQueryHandler : IRequestHandler<Queries.GetEmployeeDetailList, List<EmployeeDetailVM>>
    {
        public GetEmployeeDetailListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeDetailVM>> Handle(Queries.GetEmployeeDetailList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public.GetEmployeeDetail ({request.CompanyId})"; ;

             var data = await _connection.QueryAsync<EmployeeDetailVM>(query);
            return data.ToList();
        }
    }
}
