using CompanySetup.Application.Upazila.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Upazila.Queries
{
    public class GetUpazilaListQueryHandler : IRequestHandler<Queries.GetUpazilaList, List<UpazilaVM>>
    {
        public GetUpazilaListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<UpazilaVM>> Handle(Queries.GetUpazilaList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetUpazilaByDistrict('{request.DistrictId}')"; ;

            var data = await _connection.QueryAsync<UpazilaVM>(query);
            return data.ToList();
        }
    }
}
