using PayrollManagement.Application.BenefitDeductionCode.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.BenefitDeductionCode.Queries
{
    public class GetBenefitDeductionCodeQueryHandler : IRequestHandler<Queries.GetBenefitDeductionCode, BenefitDeductionCodeVM>
    {
        public GetBenefitDeductionCodeQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BenefitDeductionCodeVM>
            Handle(Queries.GetBenefitDeductionCode request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionCodeById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<BenefitDeductionCodeVM>
                (query);
            return data;
        }
    }
}

