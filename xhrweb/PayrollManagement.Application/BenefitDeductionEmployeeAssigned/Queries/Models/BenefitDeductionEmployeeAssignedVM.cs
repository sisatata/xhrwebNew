using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitDeductionEmployeeAssigned.Queries.Models
{
    public class BenefitDeductionEmployeeAssignedVM
    {
        public Guid? Id { get; set; }
        public Guid? BenefitDeductionId { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Remarks { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Amount { get; set; }
        public string BenefitDeductionCode { get; set; }

        public string EmployeeName { get; set; }
    }
}
