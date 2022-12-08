using CompanySetup.Application.Branch.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Branch.Queries
{
    public class GetBranchByCompanyQueryHandler : IRequestHandler<Queries.GetBranchByCompany, List<BranchVM>>
    {
        public GetBranchByCompanyQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<BranchVM>> Handle(Queries.GetBranchByCompany request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetBranchByCompany('{request.CompanyId}')";

            var data = await _connection.QueryAsync<BranchVM>(query);
            return data.ToList();
        }
    }
}
