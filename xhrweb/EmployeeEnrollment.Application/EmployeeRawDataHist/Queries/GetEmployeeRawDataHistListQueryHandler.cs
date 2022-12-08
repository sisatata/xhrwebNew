using EmployeeEnrollment.Application.EmployeeRawDataHist.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeRawDataHist.Queries
{
    public class GetEmployeeRawDataHistListQueryHandler : IRequestHandler<Queries.GetEmployeeRawDataHistList, List<EmployeeRawDataHistVM>>
    {
        public GetEmployeeRawDataHistListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeRawDataHistVM>> Handle(Queries.GetEmployeeRawDataHistList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeRawDataHistById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<EmployeeRawDataHistVM>(query);
            return data.ToList();
        }
    }
}
