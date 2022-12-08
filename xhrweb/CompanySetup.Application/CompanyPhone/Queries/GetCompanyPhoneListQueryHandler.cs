using CompanySetup.Application.CompanyPhone.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.CompanyPhone.Queries
{
    public class GetCompanyPhoneListQueryHandler : IRequestHandler<Queries.GetCompanyPhoneList, List<CompanyPhoneVM>>
    {
        public GetCompanyPhoneListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CompanyPhoneVM>> Handle(Queries.GetCompanyPhoneList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main. GetCompanyPhoneByCompany ('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<CompanyPhoneVM>(query);
            return data.ToList();
        }
    }
}
