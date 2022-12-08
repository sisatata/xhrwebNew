using PayrollManagement.Application.SalaryStructure.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.SalaryStructure.Queries
{
    public class GetSalaryStructureQueryHandler : IRequestHandler<Queries.GetSalaryStructure, SalaryStructureVM>
    {
        public GetSalaryStructureQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<SalaryStructureVM>
            Handle(Queries.GetSalaryStructure request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetSalaryStructureById ({request.Id})";

            var data = await _connection.QueryFirstAsync<SalaryStructureVM>
                (query);
            return data;
        }
    }
}

