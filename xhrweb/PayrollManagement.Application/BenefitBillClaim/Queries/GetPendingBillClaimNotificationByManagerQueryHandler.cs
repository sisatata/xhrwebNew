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
    public class GetPendingBillClaimNotificationByManagerQueryHandler : IRequestHandler<Queries.GetPendingBillClaimNotificationByManager, List<BillClaimNotificationVM>>
    {
        public GetPendingBillClaimNotificationByManagerQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;
        public async Task<List<BillClaimNotificationVM>> Handle(Queries.GetPendingBillClaimNotificationByManager request, CancellationToken cancellationToken)
        {
            if (!request.StartTime.HasValue)
            {
                request.StartTime = DateTime.Now.Date.AddDays(-30);
            }

            if (!request.EndTime.HasValue)
            {
                request.EndTime = DateTime.Now;
            }
            DateTime format = (DateTime)request.EndTime;
            string endDate = format.ToString("yyyy-MM-dd");
            format = (DateTime)request.StartTime;
            string startDate = format.ToString("yyyy-MM-dd");
            var query = $"SELECT * from payroll.GetPendingBillClaimApproveQueue('{request.ManagerId}','{startDate}', '{endDate}')";

            var data = await _connection.QueryAsync<BillClaimNotificationVM>(query);
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
