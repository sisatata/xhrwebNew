using CompanySetup.Application.Grade.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace CompanySetup.Application.Grade.Queries
{
    public class GetGradeQueryHandler : IRequestHandler<Queries.GetGrade, GradeVM>
    {
        public GetGradeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<GradeVM>
            Handle(Queries.GetGrade request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from main.GetGradeById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<GradeVM>
                (query);
            return data;
        }
    }
}

