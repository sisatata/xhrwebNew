using CompanySetup.Application.OfficeNotice.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.OfficeNotice.Queries
{
    public class GetOfficeNoticeQueryHandler : IRequestHandler<Queries.GetOfficeNotice, OfficeNoticeVM>
    {
        public GetOfficeNoticeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<OfficeNoticeVM>
            Handle(Queries.GetOfficeNotice request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from  main.GetOfficeNoticeById ('{request.Id}')";

            var data = await _connection.QueryFirstAsync<OfficeNoticeVM> (query);
            return data;
        }
    }
}

