using EmployeeEnrollment.Application.EmployeeAddress.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeAddress.Queries
{
    public class GetEmployeeAddressQueryHandler : IRequestHandler<Queries.GetEmployeeAddress, EmployeeAddressVM>
    {
        public GetEmployeeAddressQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeAddressVM>
            Handle(Queries.GetEmployeeAddress request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.getemployeeaddressbyid('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeAddressVM>
                (query);
            return data;
        }
    }
}

