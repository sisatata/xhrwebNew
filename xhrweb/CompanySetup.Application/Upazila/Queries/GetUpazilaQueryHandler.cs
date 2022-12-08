using CompanySetup.Application.Upazila.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.Upazila.Queries
{
    public class GetUpazilaQueryHandler : IRequestHandler<Queries.GetUpazila, UpazilaVM>
    {
        public GetUpazilaQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<UpazilaVM>
            Handle(Queries.GetUpazila request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetUpazilaById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<UpazilaVM>
                (query);
            return data;
        }
    }
}

