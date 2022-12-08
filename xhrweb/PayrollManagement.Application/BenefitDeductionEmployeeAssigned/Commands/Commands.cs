using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateBenefitDeductionEmployeeAssigned : IRequest<BenefitDeductionEmployeeAssignedCommandVM>
            {

                public Guid? BenefitDeductionId { get; set; }
                public Guid? EmployeeId { get; set; }
                public string Remarks { get; set; }
                public Boolean IsDeleted { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public decimal? Amount { get; set; }
            }

            public class UpdateBenefitDeductionEmployeeAssigned : IRequest<BenefitDeductionEmployeeAssignedCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? BenefitDeductionId { get; set; }
                public Guid? EmployeeId { get; set; }
                public string Remarks { get; set; }
                public Boolean IsDeleted { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public decimal? Amount { get; set; }
            }

            public class MarkAsDeleteBenefitDeductionEmployeeAssigned : IRequest<BenefitDeductionEmployeeAssignedCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
