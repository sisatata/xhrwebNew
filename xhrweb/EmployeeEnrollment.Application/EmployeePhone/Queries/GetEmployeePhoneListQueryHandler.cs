using EmployeeEnrollment.Application.EmployeePhone.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeePhone.Queries
{
    public class GetEmployeePhoneListQueryHandler : IRequestHandler<Queries.GetEmployeePhoneList, List<EmployeePhoneVM>>
    {
        public GetEmployeePhoneListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeePhoneVM>> Handle(Queries.GetEmployeePhoneList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeePhoneByEmployee ('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeePhoneVM>(query);
            return data.ToList();
        }
    }
}
