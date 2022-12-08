using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Queries.Models
{
    public class EmployeeSalaryProcessedDataComponentVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeSalaryProcessedDataId { get; set; }
        public Guid? EmployeeSalaryComponentId { get; set; }
        public Guid? BenefitDeductionId { get; set; }
        public decimal? ComponentValue { get; set; }
        public string Type { get; set; }
        public Guid? CompanyId { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
