using EmployeeEnrollment.Application.EmployeeManager.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeManager.Queries
{
    public class GetEmployeeManagerQueryHandler : IRequestHandler<Queries.GetEmployeeManager, EmployeeManagerVM>
    {
        public GetEmployeeManagerQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeManagerVM>
            Handle(Queries.GetEmployeeManager request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.getemployeemanagerbyemployee('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeManagerVM>
                (query);
            return data;
        }
    }
}

