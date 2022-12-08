using EmployeeEnrollment.Application.RawFileDetail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.RawFileDetail.Queries
{
    public class GetRawFileDetailQueryHandler : IRequestHandler<Queries.GetRawFileDetail, RawFileDetailVM>
    {
        public GetRawFileDetailQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<RawFileDetailVM>
            Handle(Queries.GetRawFileDetail request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetRawFileDetailById ({request.Id})";

            var data = await _connection.QueryFirstAsync<RawFileDetailVM>
                (query);
            return data;
        }
    }
}

