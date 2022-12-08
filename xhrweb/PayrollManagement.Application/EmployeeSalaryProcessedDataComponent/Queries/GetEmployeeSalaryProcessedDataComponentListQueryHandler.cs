using PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries
{
    public class GetEmployeeSalaryProcessedDataComponentListQueryHandler : IRequestHandler<Queries.GetEmployeeSalaryProcessedDataComponentList, List<EmployeeSalaryProcessedDataComponentVM>>
    {
        public GetEmployeeSalaryProcessedDataComponentListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeSalaryProcessedDataComponentVM>> Handle(Queries.GetEmployeeSalaryProcessedDataComponentList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetEmployeeSalaryProcessedDataComponentById ({request.CompanyId})"; ;

            var data = await _connection.QueryAsync<EmployeeSalaryProcessedDataComponentVM>(query);
            return data.ToList();
        }
    }
}
