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
    public class GetBenefitBillClaimListByEmployeeQueryHandler : IRequestHandler<Queries.GetBenefitBillClaimListByEmployee, BenefitBillClaimDataVM>
    {
        public GetBenefitBillClaimListByEmployeeQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;

        public async Task<BenefitBillClaimDataVM> Handle(Queries.GetBenefitBillClaimListByEmployee request, CancellationToken cancellationToken)
        {
            BenefitBillClaimDataVM oModel = new BenefitBillClaimDataVM();
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from payroll.GetBenefitBillClaimByEmployee('{request.EmployeeId}','{startDate}','{endDate}')"; ;

            var data = await _connection.QueryAsync<BenefitBillClaimVM>(query);
            foreach (var d in data)
            {
                // Combining real path
                if (d != null && !string.IsNullOrWhiteSpace(d.BillAttachmentName))
                {
                    d.BillAttachmentUri = _uriComposer.ComposeAttachedFileUri(d.BillAttachmentName);
                }
            }
            oModel.BenefitBillClaims = data.ToList();
            
            var summary = $"SELECT * from payroll.GetBenefitBillClaimSummaryByEmployee('{request.EmployeeId}','{startDate}','{endDate}')"; ;

            var summaryData = await _connection.QueryAsync<BenefitBillClaimSummaryVM>(summary);
            oModel.BenefitBillClaimSummaries = summaryData.ToList();

            return oModel;
        }
    }
}
