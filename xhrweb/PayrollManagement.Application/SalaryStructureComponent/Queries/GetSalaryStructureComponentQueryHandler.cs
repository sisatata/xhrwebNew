using PayrollManagement.Application.SalaryStructureComponent.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.SalaryStructureComponent.Queries
{
    public class GetSalaryStructureComponentQueryHandler : IRequestHandler<Queries.GetSalaryStructureComponent, SalaryStructureComponentVM>
    {
        public GetSalaryStructureComponentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<SalaryStructureComponentVM>
            Handle(Queries.GetSalaryStructureComponent request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetSalaryStructureComponentById ({request.Id})";

            var data = await _connection.QueryFirstAsync<SalaryStructureComponentVM>
                (query);
            return data;
        }
    }
}

