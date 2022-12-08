using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalary.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeSalary : IRequest<EmployeeSalaryCommandVM>
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

            }

            public class UpdateEmployeeSalary : IRequest<EmployeeSalaryCommandVM>
            {
                public Guid? Id { get; set; }
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
            }

            public class MarkAsDeleteEmployeeSalary : IRequest<EmployeeSalaryCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
