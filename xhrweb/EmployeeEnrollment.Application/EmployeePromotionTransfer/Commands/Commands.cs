using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeePromotionTransfer.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeePromotionTransfer : IRequest<EmployeePromotionTransferCommandVM>
            {
                public Guid? EmployeeId { get; set; }
                public Guid? PreviousCompanyId { get; set; }
                public Guid? PreviousBranchId { get; set; }
                public Guid? PreviousDepartmentId { get; set; }
                public Guid? PreviousPositionId { get; set; }
                public Guid? NewCompanyId { get; set; }
                public Guid? NewBranchId { get; set; }
                public Guid? NewDepartmentId { get; set; }
                public Guid? NewPositionId { get; set; }
                public DateTime? ProposedDate { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public decimal? PreviousGross { get; set; }
                public decimal? NewGross { get; set; }
                public decimal? PreviousBasic { get; set; }
                public decimal? NewBasic { get; set; }
                public decimal? PreviousHouserent { get; set; }
                public decimal? NewHouserent { get; set; }
                public Int32? IncrementTypeId { get; set; }
                public int IncrementValueTypeId { get; set; }
                public decimal? IncrementValue { get; set; }
                public decimal? IncrementAmount { get; set; }
                public Int32? IncrementOn { get; set; }
                public string Reason { get; set; }
                public string Reference { get; set; }
                //public DateTime? ApproveDate { get; set; }
                //public Guid? ApproveBy { get; set; }
                //public string ApproveNote { get; set; }
                //public string ApprovalStatus { get; set; }
                public Guid? PreviousGradeId { get;  set; }
                public Guid? NewGradeId { get;  set; }
                public Guid? PreviousSalaryStructureId { get;  set; }
                public Guid? NewSalaryStructureId { get;  set; }
                public short? PreviousPaymentOptionId { get;  set; }
                public short? NewPaymentOptionId { get;  set; }
            }

            public class UpdateEmployeePromotionTransfer : IRequest<EmployeePromotionTransferCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public Guid? PreviousCompanyId { get; set; }
                public Guid? PreviousBranchId { get; set; }
                public Guid? PreviousDepartmentId { get; set; }
                public Guid? PreviousPositionId { get; set; }
                public Guid? NewCompanyId { get; set; }
                public Guid? NewBranchId { get; set; }
                public Guid? NewDepartmentId { get; set; }
                public Guid? NewPositionId { get; set; }
                public DateTime? ProposedDate { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public decimal? PreviousGross { get; set; }
                public decimal? NewGross { get; set; }
                public decimal? PreviousBasic { get; set; }
                public decimal? NewBasic { get; set; }
                public decimal? PreviousHouserent { get; set; }
                public decimal? NewHouserent { get; set; }
                public Int32? IncrementTypeId { get; set; }
                public int IncrementValueTypeId { get; set; }
                public decimal? IncrementValue { get; set; }
                public decimal? IncrementAmount { get; set; }
                public Int32? IncrementOn { get; set; }
                public string Reason { get; set; }
                public string Reference { get; set; }
                //public DateTime? ApproveDate { get; set; }
                //public Guid? ApproveBy { get; set; }
                //public string ApproveNote { get; set; }
                //public string ApprovalStatus { get; set; }
                public Guid? PreviousGradeId { get;  set; }
                public Guid? NewGradeId { get;  set; }
                public Guid? PreviousSalaryStructureId { get;  set; }
                public Guid? NewSalaryStructureId { get;  set; }
                public short? PreviousPaymentOptionId { get;  set; }
                public short? NewPaymentOptionId { get;  set; }
            }

            public class MarkAsDeleteEmployeePromotionTransfer : IRequest<EmployeePromotionTransferCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class ApproveEmployeePromotionTransferCommand : IRequest<EmployeePromotionTransferCommandVM>
            {
                public Guid EmployeePromotionTransferId { get; set; }
                public string Notes { get; set; }
            }
            public class RejectEmployeePromotionTransferCommand : IRequest<EmployeePromotionTransferCommandVM>
            {
                public Guid EmployeePromotionTransferId { get; set; }
                public string Notes { get; set; }
            }

        }
    }
}
