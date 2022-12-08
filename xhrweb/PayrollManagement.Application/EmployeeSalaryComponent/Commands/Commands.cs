using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalaryComponent.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeSalaryComponent : IRequest<EmployeeSalaryComponentCommandVM>
            {
                public Guid? EmployeeSalaryId { get; set; }
                public Guid? SalaryStructureComponentId { get; set; }
                public decimal? ComponentAmount { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class UpdateEmployeeSalaryComponent : IRequest<EmployeeSalaryComponentCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeSalaryId { get; set; }
                public Guid? SalaryStructureComponentId { get; set; }
                public decimal? ComponentAmount { get; set; }
                public Guid? CompanyId { get; set; }
                public Boolean IsDeleted { get; set; }
            }

            public class MarkAsDeleteEmployeeSalaryComponent : IRequest<EmployeeSalaryComponentCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
