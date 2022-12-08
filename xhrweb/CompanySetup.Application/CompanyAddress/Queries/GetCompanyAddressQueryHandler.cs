using CompanySetup.Application.CompanyAddress.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.CompanyAddress.Queries
{
    public class GetCompanyAddressQueryHandler : IRequestHandler<Queries.GetCompanyAddress, CompanyAddressVM>
    {
        public GetCompanyAddressQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<CompanyAddressVM>
            Handle(Queries.GetCompanyAddress request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetCompanyAddressById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<CompanyAddressVM>
                (query);
            return data;
        }
    }
}

