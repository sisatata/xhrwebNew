using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalaryProcessedDataComponent.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeSalaryProcessedDataComponent : IRequest<EmployeeSalaryProcessedDataComponentCommandVM>
            {
                public Guid? EmployeeSalaryProcessedDataId { get; set; }
                public Guid? EmployeeSalaryComponentId { get; set; }
                public Guid? BenefitDeductionId { get; set; }
                public decimal? ComponentValue { get; set; }
                public string Type { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class UpdateEmployeeSalaryProcessedDataComponent : IRequest<EmployeeSalaryProcessedDataComponentCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? EmployeeSalaryProcessedDataId { get; set; }
                public Guid? EmployeeSalaryComponentId { get; set; }
                public Guid? BenefitDeductionId { get; set; }
                public decimal? ComponentValue { get; set; }
                public string Type { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeleteEmployeeSalaryProcessedDataComponent : IRequest<EmployeeSalaryProcessedDataComponentCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
