using EmployeeEnrollment.Application.EmployeeEducation .Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeEducation .Queries
{
    public class GetEmployeeEducationListQueryHandler : IRequestHandler< Queries. GetEmployeeEducationList, List< EmployeeEducationVM>>
    {
        public GetEmployeeEducationListQueryHandler (DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task< List< EmployeeEducationVM>> Handle(Queries.GetEmployeeEducationList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeEducationByEmployee ('{request.EmployeeId}')";;

            var data = await _connection.QueryAsync< EmployeeEducationVM>(query);
            return data.ToList();
        }
    }
}
