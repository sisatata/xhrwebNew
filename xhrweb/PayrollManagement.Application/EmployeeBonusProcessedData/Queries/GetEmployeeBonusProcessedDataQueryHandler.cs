using PayrollManagement.Application.EmployeeBonusProcessedData.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeeBonusProcessedData.Queries
{
    public class GetEmployeeBonusProcessedDataQueryHandler : IRequestHandler<Queries.GetEmployeeBonusProcessedData, EmployeeBonusProcessedDataVM>
    {
        public GetEmployeeBonusProcessedDataQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeBonusProcessedDataVM>
            Handle(Queries.GetEmployeeBonusProcessedData request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.EmployeeBonusProcessedDataById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeBonusProcessedDataVM>
                (query);
            return data;
        }
    }
}

