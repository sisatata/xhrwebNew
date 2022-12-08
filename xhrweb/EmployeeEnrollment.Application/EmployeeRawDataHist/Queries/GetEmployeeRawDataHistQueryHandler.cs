using EmployeeEnrollment.Application.EmployeeRawDataHist.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeRawDataHist.Queries
{
    public class GetEmployeeRawDataHistQueryHandler : IRequestHandler<Queries.GetEmployeeRawDataHist, EmployeeRawDataHistVM>
    {
        public GetEmployeeRawDataHistQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeRawDataHistVM>
            Handle(Queries.GetEmployeeRawDataHist request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEployeeRawDataHistById ({request.Id})";

            var data = await _connection.QueryFirstAsync<EmployeeRawDataHistVM>
                (query);
            return data;
        }
    }
}

