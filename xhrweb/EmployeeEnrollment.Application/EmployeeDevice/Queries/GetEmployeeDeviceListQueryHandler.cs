using EmployeeEnrollment.Application.EmployeeDevice.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeDevice.Queries
{
    public class GetEmployeeDeviceListQueryHandler : IRequestHandler<Queries.GetEmployeeDeviceList, List<EmployeeDeviceVM>>
    {
        public GetEmployeeDeviceListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeDeviceVM>> Handle(Queries.GetEmployeeDeviceList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeDeviceById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<EmployeeDeviceVM>(query);
            return data.ToList();
        }
    }
}
