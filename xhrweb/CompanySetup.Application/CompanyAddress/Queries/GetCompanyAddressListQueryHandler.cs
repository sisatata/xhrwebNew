using CompanySetup.Application.CompanyAddress.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.CompanyAddress.Queries
{
    public class GetCompanyAddressListQueryHandler : IRequestHandler<Queries.GetCompanyAddressList, List<CompanyAddressVM>>
    {
        public GetCompanyAddressListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<CompanyAddressVM>> Handle(Queries.GetCompanyAddressList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetCompanyAddressByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<CompanyAddressVM>(query);
            return data.ToList();
        }
    }
}
