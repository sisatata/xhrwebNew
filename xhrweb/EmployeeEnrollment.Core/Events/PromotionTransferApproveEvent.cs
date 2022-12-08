using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Events
{
    public class PromotionTransferApproveEvent : ICommand
    {
        public Guid? EmployeeId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? GradeId { get; set; }
        public Guid? SalaryStructureId { get; set; }
        public Int16? PaymentOptionId { get; set; }
        public decimal? GrossSalary { get; set; }
        public Guid? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public Boolean IsRequestFromPromotion { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
