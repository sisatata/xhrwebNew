using EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Queries
{
    public class GetPreviousPFGratuityEarnLeaveListQueryHandler : IRequestHandler<Queries.GetPreviousPFGratuityEarnLeaveList, List<PreviousPFGratuityEarnLeaveVM>>
    {
        public GetPreviousPFGratuityEarnLeaveListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<PreviousPFGratuityEarnLeaveVM>> Handle(Queries.GetPreviousPFGratuityEarnLeaveList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetPreviousPFGratuityEarnLeaveByCompanyId('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<PreviousPFGratuityEarnLeaveVM>(query);
            return data.ToList();
        }
    }
}
