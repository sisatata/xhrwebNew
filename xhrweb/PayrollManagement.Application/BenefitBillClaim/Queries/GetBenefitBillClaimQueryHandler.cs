using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ASL.Hrms.SharedKernel.Interfaces;

namespace PayrollManagement.Application.BenefitBillClaim.Queries
{
    public class GetBenefitBillClaimQueryHandler : IRequestHandler<Queries.GetBenefitBillClaim, BenefitBillClaimVM>
    {
        public GetBenefitBillClaimQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;

        public async Task<BenefitBillClaimVM>
            Handle(Queries.GetBenefitBillClaim request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * from payroll.GetBenefitBillClaimById ('{request.Id}')";

            var data = await _connection.QueryFirstOrDefaultAsync<BenefitBillClaimVM> (query);

            data.BillAttachmentUri = _uriComposer.ComposeAttachedFileUri(data.BillAttachmentName);

            return data;
        }
    }
}

