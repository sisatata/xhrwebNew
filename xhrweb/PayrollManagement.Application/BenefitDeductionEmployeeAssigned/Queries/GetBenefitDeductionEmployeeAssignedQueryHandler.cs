using PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries
{
    public class GetBenefitDeductionEmployeeAssignedQueryHandler : IRequestHandler<Queries.GetBenefitDeductionEmployeeAssigned, BenefitDeductionEmployeeAssignedVM>
    {
        public GetBenefitDeductionEmployeeAssignedQueryHandler(DbConnection connection)
        {
            _connection = connection;
        }
        private readonly DbConnection _connection;

        public async Task<BenefitDeductionEmployeeAssignedVM>
            Handle(Queries.GetBenefitDeductionEmployeeAssigned request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitDeductionEmployeeAssignedById('{request.Id}')";

            var data = await _connection.QueryFirstAsync<BenefitDeductionEmployeeAssignedVM>
                (query);
            return data;
        }
    }
}

