using PayrollManagement.Application.EmployeePaidIncomeTax.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.EmployeePaidIncomeTax.Queries
{
    public class GetEmployeePaidIncomeTaxQueryHandler : IRequestHandler<Queries.GetEmployeePaidIncomeTax, EmployeePaidIncomeTaxVM>
    {
        public GetEmployeePaidIncomeTaxQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeePaidIncomeTaxVM>
            Handle(Queries.GetEmployeePaidIncomeTax request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetEmployeePaidIncomeTaxById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeePaidIncomeTaxVM>
                (query);
            return data;
        }
    }
}

