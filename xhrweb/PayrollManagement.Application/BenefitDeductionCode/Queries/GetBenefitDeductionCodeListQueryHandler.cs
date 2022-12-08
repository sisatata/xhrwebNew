using PayrollManagement.Application.BenefitDeductionCode.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BenefitDeductionCode.Queries
{
    public class GetBenefitDeductionCodeListQueryHandler : IRequestHandler<Queries.GetBenefitDeductionCodeList, List<BenefitDeductionCodeVM>>
    {
        public GetBenefitDeductionCodeListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BenefitDeductionCodeVM>> Handle(Queries.GetBenefitDeductionCodeList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionCodeByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<BenefitDeductionCodeVM>(query);
            return data.ToList();
        }
    }
}
