using CompanySetup.Application.MonthCycle.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.MonthCycle.Queries
{
  public  class GetMonthCycleByIdQueryHandler: IRequestHandler<Queries.GetMonthCycleById, MonthCycleVM>
    {
        public GetMonthCycleByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<MonthCycleVM> Handle(Queries.GetMonthCycleById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetMonthCyclebyId('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<MonthCycleVM>(query);
            return data;
        }
    }
}
