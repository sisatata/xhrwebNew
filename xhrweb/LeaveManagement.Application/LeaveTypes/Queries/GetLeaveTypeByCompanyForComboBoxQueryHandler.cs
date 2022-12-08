using Dapper;
using LeaveManagement.Application.LeaveTypes.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveManagement.Application.LeaveTypes.Queries
{
    public class GetLeaveTypeByCompanyForComboBoxQueryHandler : IRequestHandler<Queries.GetLeaveTypeByCompanyForComboBox, List<LeaveType>>
    {
        public GetLeaveTypeByCompanyForComboBoxQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        private readonly DbConnection _connection;

        public async Task<List<LeaveType>> Handle(Queries.GetLeaveTypeByCompanyForComboBox request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from leave.GetLeaveTypeByCompany('{request.CompanyId}')";

            var data = await _connection.QueryAsync<LeaveType>(query).ConfigureAwait(false);

            List<LeaveType> distinctLeaveTypes = data
                          .GroupBy(p => new { p.LeaveTypeShortName, p.LeaveTypeName })
                          .Select(g => g.First())
                          .ToList();

            return distinctLeaveTypes.ToList();
        }
    }
}
