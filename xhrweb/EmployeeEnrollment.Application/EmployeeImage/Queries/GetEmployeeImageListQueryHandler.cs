using EmployeeEnrollment.Application.EmployeeImage.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeImage.Queries
{
    public class GetEmployeeImageListQueryHandler : IRequestHandler<Queries.GetEmployeeImageList, List<EmployeeImageVM>>
    {
        public GetEmployeeImageListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeImageVM>> Handle(Queries.GetEmployeeImageList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeImageById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<EmployeeImageVM>(query);
            return data.ToList();
        }
    }
}
