using EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries
{
    public class GetPreviousPFGratuityEarnLeaveQueryHandler : IRequestHandler<Queries.GetPreviousPFGratuityEarnLeave, PreviousPFGratuityEarnLeaveVM>
    {
        public GetPreviousPFGratuityEarnLeaveQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<PreviousPFGratuityEarnLeaveVM>
            Handle(Queries.GetPreviousPFGratuityEarnLeave request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee. GetPreviousPFGratuityEarnLeaveByEmployeeId ('{request.EmployeeId}')";

            var data = await _connection.QueryFirstAsync<PreviousPFGratuityEarnLeaveVM>
                (query);
            return data;
        }
    }
}

