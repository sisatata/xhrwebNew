using CompanySetup.Application.Department.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Department.Queries
{
    public class GetDepartmentByBranchQueryHandler : IRequestHandler<Queries.GetDepartmentByBranch, List<DepartmentVM>>
    {
        public GetDepartmentByBranchQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<DepartmentVM>> Handle(Queries.GetDepartmentByBranch request, CancellationToken cancellationToken)
        {
            try
            {
                var query = $"SELECT * from main.GetDepartmentByBranch('{request.BranchId}')";

                var data = await _connection.QueryAsync<DepartmentVM>(query);
                return data.ToList();
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
