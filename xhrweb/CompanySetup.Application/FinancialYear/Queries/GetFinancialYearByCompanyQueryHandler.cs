using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanySetup.Application.FinancialYear.Queries.Models;
using Dapper;
using MediatR;

namespace CompanySetup.Application.FinancialYear.Queries
{
  public   class GetFinancialYearByCompanyQueryHandler:IRequestHandler<Queries.GetFinancialYearByCompanyId,List<FinancialYearVM>>
    {
        public GetFinancialYearByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<FinancialYearVM>> Handle(Queries.GetFinancialYearByCompanyId request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetFinancialYearbyCompanyId('{request.CompanyId}')";

            var data = await _connection.QueryAsync<FinancialYearVM>(query);
            return data.ToList();
        }
    }
}
