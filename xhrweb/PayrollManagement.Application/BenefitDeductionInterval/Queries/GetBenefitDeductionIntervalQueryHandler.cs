using PayrollManagement.Application.BenefitDeductionInterval.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.BenefitDeductionInterval.Queries
{
    public class GetBenefitDeductionIntervalQueryHandler : IRequestHandler<Queries.GetBenefitDeductionInterval, BenefitDeductionIntervalVM>
    {
        public GetBenefitDeductionIntervalQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BenefitDeductionIntervalVM>
            Handle(Queries.GetBenefitDeductionInterval request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from public. GetBenefitDeductionIntervalById ({request.Id})";

            var data = await _connection.QueryFirstAsync<BenefitDeductionIntervalVM>
                (query);
            return data;
        }
    }
}

