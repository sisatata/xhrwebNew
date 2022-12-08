using EmployeeEnrollment.Application.EmployeeNote.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.EmployeeNote.Queries
{
    public class GetEmployeeNoteListQueryHandler : IRequestHandler<Queries.GetEmployeeNoteList, List<EmployeeNoteVM>>
    {
        public GetEmployeeNoteListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<EmployeeNoteVM>> Handle(Queries.GetEmployeeNoteList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeNoteListByCompanyId('{request.EmployeeId}')"; ;

            var data = await _connection.QueryAsync<EmployeeNoteVM>(query);
            return data.ToList();
        }
    }
}
