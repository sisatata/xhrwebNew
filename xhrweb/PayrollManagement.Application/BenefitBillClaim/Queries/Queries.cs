using PayrollManagement.Application.BenefitBillClaim.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.BenefitBillClaim.Queries
{
    public static class Queries
    {
        public class GetBenefitBillClaimListByCompany : IRequest<List<BenefitBillClaimVM>>
        {
            public Guid CompanyId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class GetBenefitBillClaimListByEmployee : IRequest<BenefitBillClaimDataVM>
        {
            public Guid EmployeeId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        public class GetBenefitBillClaim : IRequest<BenefitBillClaimVM>
        {
            public Guid Id { get; set; }
        }

        public class GetPendingBillClaimNotificationByManager : IRequest<List<BillClaimNotificationVM>>
        {
            public Guid ManagerId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class GetBenefitBillClaimReportDataByCompany : IRequest<List<ReportBenefitBillClaimVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid EmployeeId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }
        public class GetPendingBillClaimNotification : IRequest<List<BillClaimNotificationVM>>
        {
            public Guid ManagerId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
          //  public Guid? CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public string SearchText { get; set; }
        }

        public class GetUnpaidApprovedBillClaimNotificationByManager : IRequest<List<BillClaimPaymentNotificationVM>>
        {
            public Guid ManagerId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

        public class GetPaidApprovedBillClaimNotificationByManager : IRequest<List<BillClaimPaymentNotificationVM>>
        {
            public Guid ManagerId { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
        }

    }
}
