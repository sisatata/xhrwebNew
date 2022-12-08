using EmployeeEnrollment.Application.EmployeeExperience.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeExperience.Queries
{
    public class GetEmployeeExperienceListQueryHandler : IRequestHandler<Queries.GetEmployeeExperienceList, List<EmployeeExperienceVM>>
    {
        public GetEmployeeExperienceListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeExperienceVM>> Handle(Queries.GetEmployeeExperienceList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeExperienceByEmployee('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeExperienceVM>(query);
            return data.ToList();
        }
    }
}
