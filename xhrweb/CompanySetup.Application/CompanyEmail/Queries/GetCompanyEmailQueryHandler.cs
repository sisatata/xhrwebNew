using CompanySetup.Application.CompanyEmail.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.CompanyEmail.Queries
{
    public class GetCompanyEmailQueryHandler : IRequestHandler<Queries.GetCompanyEmail, CompanyEmailVM>
    {
        public GetCompanyEmailQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<CompanyEmailVM>
            Handle(Queries.GetCompanyEmail request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetCompanyEmailById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<CompanyEmailVM>
                (query);
            return data;
        }
    }
}

