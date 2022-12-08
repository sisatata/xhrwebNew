using CompanySetup.Application.OfficeNotice.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.OfficeNotice.Queries
{
    public class GetOfficeNoticeActiveAndPublishedListQueryHandler : IRequestHandler<Queries.GetOfficeNoticeActiveAndPublishedList, List<OfficeNoticeVM>>
    {
        public GetOfficeNoticeActiveAndPublishedListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<OfficeNoticeVM>> Handle(Queries.GetOfficeNoticeActiveAndPublishedList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetOfficeNoticeActiveAndPublishedByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<OfficeNoticeVM>(query);
            return data.ToList();
        }
    }
}
