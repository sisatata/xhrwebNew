using CompanySetup.Application.FinancialYear.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.FinancialYear.Queries
{
    public class GetFinancialYearIdByYearNameQueryHandler : IRequestHandler<Queries.GetFinancialYearByName, FinancialYearIdVM>
    {
        public GetFinancialYearIdByYearNameQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;
        public async Task<FinancialYearIdVM> Handle(Queries.GetFinancialYearByName request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetFinancialYearidByYearname('{request.CompanyId}','{request.FinancialYearName}')";

            var data = await _connection.QueryAsync<FinancialYearIdVM>(query);
            return data.FirstOrDefault();

        }
    }
}
