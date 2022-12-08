using CompanySetup.Application.Company.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Company.Queries
{
    public class GetAllCompanyQueryHandler : IRequestHandler<Queries.GetAllCompany, List<CompanyVM>>
    {
        public GetAllCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        
        private readonly DbConnection _connection;
        public async Task<List<CompanyVM>> Handle(Queries.GetAllCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetCompany()";

            var data = await _connection.QueryAsync<CompanyVM>(query);
            return data.ToList();
        }
    }
}
