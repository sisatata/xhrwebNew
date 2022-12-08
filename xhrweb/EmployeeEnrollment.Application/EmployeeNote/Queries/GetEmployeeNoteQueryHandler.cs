using EmployeeEnrollment.Application.EmployeeNote.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeNote.Queries
{
    public class GetEmployeeNoteQueryHandler : IRequestHandler<Queries.GetEmployeeNote, EmployeeNoteVM>
    {
        public GetEmployeeNoteQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<EmployeeNoteVM> Handle(Queries.GetEmployeeNote request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeNoteById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<EmployeeNoteVM> (query);
            return data;
        }
    }
}

