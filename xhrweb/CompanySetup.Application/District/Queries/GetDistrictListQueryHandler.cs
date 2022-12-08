using CompanySetup.Application.District.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.District.Queries
{
    public class GetDistrictListQueryHandler : IRequestHandler<Queries.GetDistrictList, List<DistrictVM>>
    {
        public GetDistrictListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<DistrictVM>> Handle(Queries.GetDistrictList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetDistrict()"; ;

            var data = await _connection.QueryAsync<DistrictVM>(query);
            return data.ToList();
        }
    }
}
