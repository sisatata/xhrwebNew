using CompanySetup.Application.CommonLookUpType.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.CommonLookUpType.Queries
{
    public class GetCommonLookUpTypeListQueryHandler : IRequestHandler<Queries.GetCommonLookUpTypeListByCompany, List<CommonLookUpTypeVM>>
    {
        public GetCommonLookUpTypeListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CommonLookUpTypeVM>> Handle(Queries.GetCommonLookUpTypeListByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetCommonLookupTypeByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<CommonLookUpTypeVM>(query);
            return data.ToList();
        }
    }
}
