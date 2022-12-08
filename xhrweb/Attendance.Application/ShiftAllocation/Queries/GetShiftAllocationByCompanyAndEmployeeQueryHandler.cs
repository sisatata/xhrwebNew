using Attendance.Application.ShiftAllocation.Queries.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftAllocation.Queries
{
    public class GetShiftAllocationByCompanyAndEmployeeQueryHandler : IRequestHandler<Queries.GetShiftAllocationByCompanyAndEmployee, List<ShiftAllocationVM>>
    {
        private readonly DbConnection _connection;

        public GetShiftAllocationByCompanyAndEmployeeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<ShiftAllocationVM>> Handle(Queries.GetShiftAllocationByCompanyAndEmployee request, CancellationToken cancellationToken)
        {
            try
            {

                var query = string.Format("");
                if (request.DepartmentId == Guid.Empty && request.DesignationId == Guid.Empty && request.EmployeeId == Guid.Empty)
                {
                    query = $"SELECT * from attendance.GetShiftAllocation('{request.CompanyId}','{request.BranchId}','{request.FinancialYearId}','{request.MonthCycleId}',null,null,null)";
                }

                else if (request.DesignationId == Guid.Empty && request.EmployeeId == Guid.Empty)
                {
                    query = $"SELECT * from attendance.GetShiftAllocation('{request.CompanyId}','{request.BranchId}','{request.FinancialYearId}','{request.MonthCycleId}','{request.DepartmentId}',null,null)";
                }
                else if (request.EmployeeId == Guid.Empty)
                {
                    query = $"SELECT * from attendance.GetShiftAllocation('{request.CompanyId}','{request.BranchId}','{request.FinancialYearId}','{request.MonthCycleId}','{request.DepartmentId}','{request.DesignationId}',null)";
                }
                else if (request.DepartmentId == Guid.Empty && request.DesignationId == Guid.Empty)
                {
                    query = $"SELECT * from attendance.GetShiftAllocation('{request.CompanyId}','{request.BranchId}','{request.FinancialYearId}','{request.MonthCycleId}',null,null,'{request.EmployeeId}')";
                }
                else
                {
                    query = $"SELECT * from attendance.GetShiftAllocation('{request.CompanyId}','{request.BranchId}','{request.FinancialYearId}','{request.MonthCycleId}','{request.DepartmentId}','{request.DesignationId}','{request.EmployeeId}')";
                }
                var data = await _connection.QueryAsync<ShiftAllocationVM>(query);

                return data.ToList();
            }
            catch (Exception exssss)
            {
                var ba = exssss.Message;
                return null;
            }
        }
    }
}
