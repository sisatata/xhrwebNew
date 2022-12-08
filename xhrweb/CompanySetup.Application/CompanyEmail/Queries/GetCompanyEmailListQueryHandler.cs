using CompanySetup.Application.CompanyEmail.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.CompanyEmail.Queries
{
    public class GetCompanyEmailListQueryHandler : IRequestHandler<Queries.GetCompanyEmailList, List<CompanyEmailVM>>
    {
        public GetCompanyEmailListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CompanyEmailVM>> Handle(Queries.GetCompanyEmailList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetCompanyEmailByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<CompanyEmailVM>(query);
            return data.ToList();
        }
    }
}
