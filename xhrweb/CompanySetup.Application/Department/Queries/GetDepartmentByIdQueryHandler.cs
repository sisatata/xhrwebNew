using CompanySetup.Application.Department.Queries.Models;
using Dapper;
using MediatR;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Department.Queries
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<Queries.GetDepartmentById, DepartmentVM>
    {
        public GetDepartmentByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<DepartmentVM> Handle(Queries.GetDepartmentById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.fn_cs_GetDepartmentById('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<DepartmentVM>(query);
            return data;
        }
    }
}
