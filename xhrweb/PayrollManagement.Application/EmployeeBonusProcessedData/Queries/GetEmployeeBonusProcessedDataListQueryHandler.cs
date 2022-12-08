using PayrollManagement.Application.EmployeeBonusProcessedData.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.EmployeeBonusProcessedData.Queries
{
    public class GetEmployeeBonusProcessedDataListQueryHandler : IRequestHandler<Queries.GetEmployeeBonusProcessedDataList, List<EmployeeBonusProcessedDataVM>>
    {
        public GetEmployeeBonusProcessedDataListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeBonusProcessedDataVM>> Handle(Queries.GetEmployeeBonusProcessedDataList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll. EmployeeBonusProcessedDataListByCompany ('{request.CompanyId}','{request.BonusYearId}')"; ;

            var data = await _connection.QueryAsync<EmployeeBonusProcessedDataVM>(query);
            return data.ToList();
        }
    }
}
