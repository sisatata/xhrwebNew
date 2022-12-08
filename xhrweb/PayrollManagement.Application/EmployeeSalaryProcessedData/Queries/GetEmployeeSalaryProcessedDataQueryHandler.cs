using PayrollManagement.Application.EmployeeSalaryProcessedData.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Queries
{
    public class GetEmployeeSalaryProcessedDataQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryProcessedData, EmployeeSalaryProcessedDataVM>
    {
        public GetEmployeeSalaryProcessedDataQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeSalaryProcessedDataVM>
            Handle(Queries.GetEmployeeSalaryProcessedData request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeSalaryProcessedDataById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeSalaryProcessedDataVM>
                (query);
            return data;
        }
    }
}

