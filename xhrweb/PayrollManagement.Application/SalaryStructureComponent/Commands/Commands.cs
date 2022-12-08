using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.SalaryStructureComponent.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateSalaryStructureComponent : IRequest<SalaryStructureComponentCommandVM>
            {
                public Guid? SalaryStrutureId { get; set; }
                public string SalaryComponentName { get; set; }
                public decimal? Value { get; set; }
                public string ValueType { get; set; }
                public string PercentOn { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public short SortOrder { get;  set; }
            }

            public class UpdateSalaryStructureComponent : IRequest<SalaryStructureComponentCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? SalaryStrutureId { get; set; }
                public string SalaryComponentName { get; set; }
                public decimal? Value { get; set; }
                public string ValueType { get; set; }
                public string PercentOn { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public short SortOrder { get; set; }

            }

            public class MarkAsDeleteSalaryStructureComponent : IRequest<SalaryStructureComponentCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
