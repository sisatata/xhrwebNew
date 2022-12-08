using PayrollManagement.Application.EmployeeLeaveEncashment.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Queries
{
    public class GetEmployeeLeaveEncashmentListQueryHandler : IRequestHandler<Queries.GetEmployeeLeaveEncashmentList, List<EmployeeLeaveEncashmentVM>>
    {
        public GetEmployeeLeaveEncashmentListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeLeaveEncashmentVM>> Handle(Queries.GetEmployeeLeaveEncashmentList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. GetEmployeeLeaveEncashmentById ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<EmployeeLeaveEncashmentVM>(query);
            return data.ToList();
        }
    }
}
