using EmployeeEnrollment.Application.EmployeeEnrollment.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ASL.Hrms.SharedKernel.Interfaces;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Queries
{
    public class GetEmployeeEnrollmentQueryHandler : IRequestHandler<Queries.GetEmployeeEnrollment, EmployeeEnrollmentVM>
    {
        public GetEmployeeEnrollmentQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;

        public async Task<EmployeeEnrollmentVM>
            Handle(Queries.GetEmployeeEnrollment request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from employee.GetEmployeeEnrollmentByEmployee ('{request.EmployeeId}')";

            var data = await _connection.QueryFirstOrDefaultAsync<EmployeeEnrollmentVM>
                (query);
            // Combining real path
            if (data != null && !string.IsNullOrWhiteSpace(data.EmployeeImageUri))
            {
                data.EmployeeImageUri = _uriComposer.ComposeProfilePicUri(data.EmployeeImageUri);
            }
            if (data != null && !string.IsNullOrWhiteSpace(data.SignatureUri))
            {
                data.SignatureUri = _uriComposer.ComposeEmployeeSignatureUri(data.SignatureUri);
            }
            return data;
        }
    }
}

