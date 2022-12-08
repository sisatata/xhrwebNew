using EmployeeEnrollment.Application.EmployeeEducation .Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace EmployeeEnrollment.Application.EmployeeEducation .Queries
{
    public class GetEmployeeEducationQueryHandler : IRequestHandler< Queries. GetEmployeeEducation, EmployeeEducationVM>
    {
    public GetEmployeeEducationQueryHandler (DbConnection connection)
    {
    _connection = connection;
    }
    private readonly DbConnection _connection;

    public async Task< EmployeeEducationVM>
        Handle(Queries. GetEmployeeEducation request, CancellationToken cancellationToken)
        {
        var query = $"SELECT * from employee.GetEmployeeEducationById ('{request.Id}')";

        var data = await _connection.QueryFirstAsync< EmployeeEducationVM>
            (query);
            return data;
            }
            }
            }

