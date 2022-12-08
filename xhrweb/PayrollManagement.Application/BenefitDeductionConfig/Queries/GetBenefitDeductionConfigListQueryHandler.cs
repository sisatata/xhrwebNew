using PayrollManagement.Application.BenefitDeductionConfig.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BenefitDeductionConfig.Queries
{
    public class GetBenefitDeductionConfigListQueryHandler : IRequestHandler<Queries.GetBenefitDeductionConfigList, List<BenefitDeductionConfigVM>>
    {
        public GetBenefitDeductionConfigListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BenefitDeductionConfigVM>> Handle(Queries.GetBenefitDeductionConfigList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionConfigByCompany('{request.CompanyId}')"; ;

            var data = await _connection.QueryAsync<BenefitDeductionConfigVM>(query);
            return data.ToList();
        }
    }
}
