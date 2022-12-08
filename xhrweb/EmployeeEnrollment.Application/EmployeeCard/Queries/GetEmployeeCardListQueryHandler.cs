using EmployeeEnrollment.Application.EmployeeCard.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeCard.Queries
{
    public class GetEmployeeCardListQueryHandler : IRequestHandler<Queries.GetEmployeeCardList, List<EmployeeCardVM>>
    {
        public GetEmployeeCardListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeCardVM>> Handle(Queries.GetEmployeeCardList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee. GetEmployeeCardByEmployee ('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeCardVM>(query);
            return data.ToList();
        }
    }
}
