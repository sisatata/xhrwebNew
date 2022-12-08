using EmployeeEnrollment.Application.RawFileDetail.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.RawFileDetail.Queries
{
    public class GetRawFileDetailListQueryHandler : IRequestHandler<Queries.GetRawFileDetailList, List<RawFileDetailVM>>
    {
        public GetRawFileDetailListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<RawFileDetailVM>> Handle(Queries.GetRawFileDetailList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetRawFileDetailById ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<RawFileDetailVM>(query);
            return data.ToList();
        }
    }
}
