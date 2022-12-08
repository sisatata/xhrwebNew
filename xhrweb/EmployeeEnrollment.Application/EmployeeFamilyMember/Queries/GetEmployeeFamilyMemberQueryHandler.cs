using EmployeeEnrollment.Application.EmployeeFamilyMember.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeFamilyMember.Queries
{
    public class GetEmployeeFamilyMemberQueryHandler : IRequestHandler<Queries.GetEmployeeFamilyMember, EmployeeFamilyMemberVM>
    {
        public GetEmployeeFamilyMemberQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeFamilyMemberVM>
            Handle(Queries.GetEmployeeFamilyMember request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeFamilyMemberById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeFamilyMemberVM>
                (query);
            return data;
        }
    }
}

