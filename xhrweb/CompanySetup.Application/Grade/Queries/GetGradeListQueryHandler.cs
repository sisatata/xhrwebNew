using CompanySetup.Application.Grade.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompanySetup.Application.Grade.Queries
{
    public class GetGradeListQueryHandler : IRequestHandler<Queries.GetGradeList, List<GradeVM>>
    {
        public GetGradeListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<GradeVM>> Handle(Queries.GetGradeList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetGradeByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<GradeVM>(query);
            return data.ToList();
        }
    }
}
