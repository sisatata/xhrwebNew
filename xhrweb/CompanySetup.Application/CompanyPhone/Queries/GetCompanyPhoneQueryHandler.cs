using CompanySetup.Application.CompanyPhone.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.CompanyPhone.Queries
{
    public class GetCompanyPhoneQueryHandler : IRequestHandler<Queries.GetCompanyPhone, CompanyPhoneVM>
    {
        public GetCompanyPhoneQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<CompanyPhoneVM>
            Handle(Queries.GetCompanyPhone request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetCompanyPhoneById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<CompanyPhoneVM>
                (query);
            return data;
        }
    }
}

