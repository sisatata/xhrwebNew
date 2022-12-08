using PayrollManagement.Application.BenefitDeductionInterval.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BenefitDeductionInterval.Queries
{
    public class GetBenefitDeductionIntervalListQueryHandler : IRequestHandler<Queries.GetBenefitDeductionIntervalList, List<BenefitDeductionIntervalVM>>
    {
        public GetBenefitDeductionIntervalListQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<List<BenefitDeductionIntervalVM>> Handle(Queries.GetBenefitDeductionIntervalList request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionInterval()"; ;

            var data = await _connection.QueryAsync<BenefitDeductionIntervalVM>(query);
            return data.ToList();
        }
    }
}
