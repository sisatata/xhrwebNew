using EmployeeEnrollment.Application.EmployeeRawDataPrep.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeRawDataPrep.Queries
{
    public class GetEmployeeRawDataPrepQueryHandler : IRequestHandler<Queries.GetEmployeeRawDataPrep, EmployeeRawDataPrepVM>
    {
        public GetEmployeeRawDataPrepQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeRawDataPrepVM>
            Handle(Queries.GetEmployeeRawDataPrep request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeRawDataPrepById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeRawDataPrepVM>
                (query);
            return data;
        }
    }
}

