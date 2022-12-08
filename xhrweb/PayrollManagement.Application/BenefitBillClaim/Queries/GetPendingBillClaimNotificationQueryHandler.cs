using ASL.Hrms.SharedKernel.Interfaces;
using Dapper;
using MediatR;
using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayrollManagement.Application.BenefitBillClaim.Queries
{
    public class GetPendingBillClaimNotificationQueryHandler : IRequestHandler<Queries.GetPendingBillClaimNotification, List<BillClaimNotificationVM>>
    {
        public GetPendingBillClaimNotificationQueryHandler(DbConnection connection, IUriComposer uriComposer)
        {
            _connection = connection;
            _uriComposer = uriComposer;
        }
        private readonly DbConnection _connection;
        private readonly IUriComposer _uriComposer;
        public async Task<List<BillClaimNotificationVM>> Handle(Queries.GetPendingBillClaimNotification request, CancellationToken cancellationToken)
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
            var query = $"SELECT * from payroll.GetPendingBillClaim('{request.ManagerId}','{startDate}', '{endDate}')";
            try
            {
                var data = await _connection.QueryAsync<BillClaimNotificationVM>(query).ConfigureAwait(false);
                if (request.BranchIds != null && request.BranchIds.Count > 0)
                {
                    data = data.Where(r => request.BranchIds.Contains(r.BranchId.Value));
                }

                if (request.DepartmentIds != null && request.DepartmentIds.Count > 0)
                {
                    data = data.Where(r => request.DepartmentIds.Contains(r.DepartmentId.Value));
                }

                if (request.DesignationIds != null && request.DesignationIds.Count > 0)
                {
                    data = data.Where(r => request.DesignationIds.Contains(r.PositionId.Value));
                }

                if (!string.IsNullOrEmpty(request.SearchText))
                {
                    request.SearchText = request.SearchText.Trim();
                    var nameList = new List<string>();
                    var emailList = new List<string>();
                    var idList = new List<string>();
                    foreach (var item in data)
                    {
                        bool employeeFullNameFound = item.ApplicantName.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeFullNameFound)
                        {

                            nameList.Add(item.ApplicantName);
                        }

                        bool employeeEmailFound = item.LoginId.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeEmailFound)
                        {

                            emailList.Add(item.LoginId);
                        }
                        bool employeeIdFound = item.CompanyEmployeeId.ToLower(CultureInfo.CurrentCulture).Contains(request.SearchText.ToLower(CultureInfo.CurrentCulture), StringComparison.Ordinal);
                        if (employeeIdFound)
                        {

                            idList.Add(item.CompanyEmployeeId);
                        }
                    }
                    data = data.Where(r => emailList.Contains(r.LoginId) || nameList.Contains(r.ApplicantName) || idList.Contains(r.CompanyEmployeeId));
                }

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

            catch (System.Exception ex)
            {

                throw new ArgumentException(ex.ToString());
            }

        }
    }
}
