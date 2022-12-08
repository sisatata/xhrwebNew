using PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries
{
    public class GetBenefitDeductionEmployeeAssignedListQueryHandler : IRequestHandler<Queries.GetBenefitDeductionEmployeeAssignedList, List<BenefitDeductionEmployeeAssignedVM>>
    {
        public GetBenefitDeductionEmployeeAssignedListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BenefitDeductionEmployeeAssignedVM>> Handle(Queries.GetBenefitDeductionEmployeeAssignedList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionEmployeeAssignedByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<BenefitDeductionEmployeeAssignedVM>(query);
            return data.ToList();
        }
    }
}
