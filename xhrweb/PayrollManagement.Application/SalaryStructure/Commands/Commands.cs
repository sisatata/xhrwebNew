using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.SalaryStructure.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateSalaryStructure : IRequest<SalaryStructureCommandVM>
            {
                public string StructureName { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateSalaryStructure : IRequest<SalaryStructureCommandVM>
            {
                public Guid Id { get; set; }
                public string StructureName { get; set; }
                public string Description { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteSalaryStructure : IRequest<SalaryStructureCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
