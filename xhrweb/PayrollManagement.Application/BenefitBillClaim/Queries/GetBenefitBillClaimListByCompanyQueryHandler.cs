using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ASL.Hrms.SharedKernel.Interfaces;
using System;

namespace PayrollManagement.Application.BenefitBillClaim.Queries
{
    public class GetBenefitBillClaimListByCompanyQueryHandler : IRequestHandler<Queries.GetBenefitBillClaimListByCompany, List<BenefitBillClaimVM>>
    {
        public GetBenefitBillClaimListByCompanyQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;
        public async Task<List<BenefitBillClaimVM>> Handle(Queries.GetBenefitBillClaimListByCompany request, CancellationToken cancellationToken)
        {
            
            string endDate = "", startDate = "";
          var  format = (DateTime)request.EndTime;
            endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            startDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from payroll.GetBenefitBillClaimByCompany('{request.CompanyId}','{startDate}','{endDate}')"; ;

            var data = await _connection.QueryAsync<BenefitBillClaimVM>(query);
            foreach (var d in data)
            {
                // Combining real path
                if (d != null && !string.IsNullOrWhiteSpace(d.BillAttachmentName))
                {
                    d.BillAttachmentUri = _uriComposer.ComposeAttachedFileUri(d.BillAttachmentName);
                }
            }

            return data.ToList();
        }
    }
}
