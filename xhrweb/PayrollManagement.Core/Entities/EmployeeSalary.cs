using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using PayrollManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace PayrollManagement.Core.Entities
{
    public class EmployeeSalary : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public Guid? BranchId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid? PositionId { get; private set; }
        public Guid? GradeId { get; private set; }
        public Guid? SalaryStructureId { get; private set; }
        public Int16? PaymentOptionId { get; private set; }
        public decimal? GrossSalary { get; private set; }
        public Guid? CompanyId { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public EmployeeSalary(Guid id) : base(id) { }
        private EmployeeSalary() : base(Guid.NewGuid()) { }

        public void Create(

         Guid? employeeId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         Guid? gradeId,
         Guid? salaryStructureId,
         Int16? paymentOptionId,
         decimal? grossSalary,
         Guid? companyId,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted

        )

        {
            EmployeeId = employeeId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            GradeId = gradeId;
            SalaryStructureId = salaryStructureId;
            PaymentOptionId = paymentOptionId;
            GrossSalary = grossSalary;
            CompanyId = companyId;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
        }


        public void UpdateEmployeeSalary
            (

         Guid? employeeId,
         Guid? branchId,
         Guid? departmentId,
         Guid? positionId,
         Guid? gradeId,
         Guid? salaryStructureId,
         Int16? paymentOptionId,
         decimal? grossSalary,
         Guid? companyId,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted

        )
        {
            EmployeeId = employeeId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            GradeId = gradeId;
            SalaryStructureId = salaryStructureId;
            PaymentOptionId = paymentOptionId;
            GrossSalary = grossSalary;
            CompanyId = companyId;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteEmployeeSalary()
        {
            IsDeleted = true;
        }

        public void UpdateEndDateEmployeeSalary(DateTime? endDate)
        {
            EndDate = endDate;
        }
    }
}

