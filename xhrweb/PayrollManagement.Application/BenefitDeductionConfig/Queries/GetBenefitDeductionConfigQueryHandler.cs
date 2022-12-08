using PayrollManagement.Application.BenefitDeductionConfig.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.BenefitDeductionConfig.Queries
{
    public class GetBenefitDeductionConfigQueryHandler : IRequestHandler<Queries.GetBenefitDeductionConfig, BenefitDeductionConfigVM>
    {
        public GetBenefitDeductionConfigQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BenefitDeductionConfigVM>
            Handle(Queries.GetBenefitDeductionConfig request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionConfigById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<BenefitDeductionConfigVM>
                (query);
            return data;
        }
    }
}

