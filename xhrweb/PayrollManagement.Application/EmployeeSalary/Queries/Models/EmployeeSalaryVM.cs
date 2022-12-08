using PayrollManagement.Application.EmployeeSalaryComponent.Queries.Models;
using PayrollManagement.Application.FinancialYear.Queries.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalary.Queries.Models
{
    public class EmployeeSalaryVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string CompanyEmployeeId { get; set; }
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
        public string BranchName { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string GradeName { get; set; }
        public string StructureName { get; set; }
        public string OptionName { get; set; }
        public List<EmployeeSalaryComponentVM> EmployeeSalaryComponentList { get; set; }

        
    }
}
