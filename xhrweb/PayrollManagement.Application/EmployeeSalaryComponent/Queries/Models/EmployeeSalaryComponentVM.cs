using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models
{
    public class EmployeeSalaryComponentVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeSalaryId { get; set; }
        public Guid? SalaryStructureComponentId { get; set; }
        public decimal? ComponentAmount { get; set; }
        public Guid? CompanyId { get; set; }
        public Boolean IsDeleted { get; set; }
        public string SalaryComponentName { get; set; }
    }
}
