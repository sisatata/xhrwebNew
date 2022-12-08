using EmployeeEnrollment.Application.EmployeeRawDataPrep.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeRawDataPrep.Queries
{
    public class GetEmployeeRawDataPrepListQueryHandler : IRequestHandler<Queries.GetEmployeeRawDataPrepList, List<EmployeeRawDataPrepVM>>
    {
        public GetEmployeeRawDataPrepListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeRawDataPrepVM>> Handle(Queries.GetEmployeeRawDataPrepList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee. GetEmployeeRawDataPrepById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<EmployeeRawDataPrepVM>(query);
            return data.ToList();
        }
    }
}
