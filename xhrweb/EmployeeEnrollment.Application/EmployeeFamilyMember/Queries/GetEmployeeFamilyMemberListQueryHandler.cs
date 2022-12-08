using EmployeeEnrollment.Application.EmployeeFamilyMember.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Queries
{
    public class GetEmployeeFamilyMemberListQueryHandler : IRequestHandler<Queries.GetEmployeeFamilyMemberList, List<EmployeeFamilyMemberVM>>
    {
        public GetEmployeeFamilyMemberListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeFamilyMemberVM>> Handle(Queries.GetEmployeeFamilyMemberList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeFamilyMemberByEmployee('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeFamilyMemberVM>(query);
            return data.ToList();
        }
    }
}
