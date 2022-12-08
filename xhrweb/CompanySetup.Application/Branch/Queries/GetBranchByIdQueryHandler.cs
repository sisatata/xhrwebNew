using CompanySetup.Application.Branch.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Branch.Queries
{
    public class GetBranchByIdQueryHandler : IRequestHandler<Queries.GetBranchById, BranchVM>
    {
        public GetBranchByIdQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<BranchVM> Handle(Queries.GetBranchById request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetBranchById('{request.BranchId}')";

            var data = await _connection.QueryFirstOrDefaultAsync<BranchVM>(query);
            return data;
        }
    }
}
