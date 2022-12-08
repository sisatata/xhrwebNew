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
    public class GetOfficeNoticeListQueryHandler : IRequestHandler<Queries.GetOfficeNoticeList, List<OfficeNoticeVM>>
    {
        public GetOfficeNoticeListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<OfficeNoticeVM>> Handle(Queries.GetOfficeNoticeList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetOfficeNoticeByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<OfficeNoticeVM>(query);
            return data.ToList();
        }
    }
}
