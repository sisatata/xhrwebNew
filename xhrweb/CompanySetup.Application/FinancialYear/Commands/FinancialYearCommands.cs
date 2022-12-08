using System;
using System.Collections.Generic;
using System.Text;
using CompanySetup.Application.FinancialYear.Commands.Models;
using MediatR;

namespace CompanySetup.Application.FinancialYear.Commands
{
   public static class FinancialYearCommands
    {
        public static class V1
        {
            public class CreateFinancialYear : IRequest<FinancialYearCommandVM>
            {
                public Guid CompanyId { get;  set; }
                public string FinancialYearName { get;  set; }
                public string FinancialYearLocalizedName { get; set; }
                public DateTime FinancialYearStartDate { get;  set; }
                public DateTime FinancialYearEndDate { get;  set; }
                public bool IsRunningYear { get;  set; }
                public short SortOrder { get;  set; }
                public bool IsDeleted { get;  set; }

            }

            public class UpdateFinancialYear : IRequest<FinancialYearCommandVM>
            {
                public Guid Id { get; set; }
                public Guid CompanyId { get;  set; }
                public string FinancialYearName { get;  set; }
                public string FinancialYearLocalizedName { get;  set; }
                public DateTime FinancialYearStartDate { get;  set; }
                public DateTime FinancialYearEndDate { get;  set; }
                public bool IsRunningYear { get;  set; }
                public short SortOrder { get;  set; }
                public bool IsDeleted { get;  set; }

            }

            public class MarkFinancialYearAsDelete : IRequest<FinancialYearCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
