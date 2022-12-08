using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanySetup.Application.FinancialYear.Queries.Models;
using Dapper;
using MediatR;

namespace CompanySetup.Application.FinancialYear.Queries
{
  public   class FinancialYearByIdQueryHandler:IRequestHandler<Queries.GetFinancialYearById,FinancialYearVM>
    {
        public FinancialYearByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<FinancialYearVM> Handle(Queries.GetFinancialYearById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetFinancialYearbyId('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<FinancialYearVM>(query);
            return data;
        }
    }
}
