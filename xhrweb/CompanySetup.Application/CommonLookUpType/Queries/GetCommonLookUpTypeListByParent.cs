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
    public class GetCommonLookUpTypeListByParentQueryHandler : IRequestHandler<Queries.GetCommonLookUpTypeListByParent, List<CommonLookUpTypeVM>>
    {
        public GetCommonLookUpTypeListByParentQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CommonLookUpTypeVM>> Handle(Queries.GetCommonLookUpTypeListByParent request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.getcommonlookuptypebyparentid ('{request.CompanyId}','{request.ParentLookUpTypeId}')";

            var data = await _connection.QueryAsync<CommonLookUpTypeVM>(query);
            return data.ToList();
        }
    }
}
