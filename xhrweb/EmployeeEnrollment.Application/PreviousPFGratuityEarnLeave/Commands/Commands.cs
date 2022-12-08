using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.PreviousPFGratuityEarnLeave.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreatePreviousPFGratuityEarnLeave : IRequest<PreviousPFGratuityEarnLeaveCommandVM>
            {
                public Guid? EmployeeId { get; set; }
                public decimal? PFAmount { get; set; }
                public decimal? GratuityAmount { get; set; }
                public decimal? EarnLeaveAmount { get; set; }
                public DateTime? TillDate { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
                public string Remarks { get; set; }
            }

            public class UpdatePreviousPFGratuityEarnLeave : IRequest<PreviousPFGratuityEarnLeaveCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public decimal? PFAmount { get; set; }
                public decimal? GratuityAmount { get; set; }
                public decimal? EarnLeaveAmount { get; set; }
                public DateTime? TillDate { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
                public string Remarks { get; set; }
            }

            public class MarkAsDeletePreviousPFGratuityEarnLeave : IRequest<PreviousPFGratuityEarnLeaveCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
