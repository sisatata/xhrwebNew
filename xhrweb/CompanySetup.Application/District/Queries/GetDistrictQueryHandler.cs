using CompanySetup.Application.District.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.District.Queries
{
    public class GetDistrictQueryHandler : IRequestHandler<Queries.GetDistrict, DistrictVM>
    {
        public GetDistrictQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<DistrictVM>
            Handle(Queries.GetDistrict request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetDistrictById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<DistrictVM>
                (query);
            return data;
        }
    }
}

