using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries
{
    public class GetEmployeeSalaryProcessedDataComponentQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryProcessedDataComponent, EmployeeSalaryProcessedDataComponentVM>
    {
        public GetEmployeeSalaryProcessedDataComponentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeSalaryProcessedDataComponentVM>
            Handle(Queries.GetEmployeeSalaryProcessedDataComponent request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeSalaryProcessedDataComponentById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeSalaryProcessedDataComponentVM>
                (query);
            return data;
        }
    }
}

