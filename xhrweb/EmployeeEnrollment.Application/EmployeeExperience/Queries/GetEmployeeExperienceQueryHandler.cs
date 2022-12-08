using EmployeeEnrollment.Application.EmployeeExperience.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeExperience.Queries
{
    public class GetEmployeeExperienceQueryHandler : IRequestHandler<Queries.GetEmployeeExperience, EmployeeExperienceVM>
    {
        public GetEmployeeExperienceQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeExperienceVM>
            Handle(Queries.GetEmployeeExperience request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeExperienceById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeExperienceVM>
                (query);
            return data;
        }
    }
}

