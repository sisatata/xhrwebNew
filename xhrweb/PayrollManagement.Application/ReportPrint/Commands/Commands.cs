using DinkToPdf.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Wkhtmltopdf.NetCore;

namespace PayrollManagement.Application.ReportPrint.Commands
{
    public static class Commands
    {
        public static class V1
        {
           public class PrintBonusReport: IRequest<string>
            {
                public Guid CompanyId { get; set; }
              
                public Guid? EmployeeId { get; set; }
                public Guid FinancialYearId { get; set; }
                public Guid BonusTypeId { get; set; }
                public IConverter Converter { get; set; }
                public IRazorViewToStringRenderer Engine { get; set; }
            }
            public class PrintOTSummaryReport : IRequest<string>
            {
                public Guid CompanyId { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public IConverter Converter { get; set; }
                public IRazorViewToStringRenderer Engine { get; set; }
            }
            public class PrintEarnLeaveEncashmentReport : IRequest<string>
            {
                public Guid CompanyId { get; set; }
                public Guid FinancialYearId { get; set; }
                public Guid MonthCycleId { get; set; }
                public IConverter Converter { get; set; }
                public IRazorViewToStringRenderer Engine { get; set; }
            }
            public class PrintProvidentFundReport : IRequest<string>
            {
                public Guid CompanyId { get; set; }
                public Guid FinancialYearId { get; set; }
                public Guid MonthCycleId { get; set; }

                public Guid? EmployeeId { get; set; }
                public IConverter Converter { get; set; }
                public IRazorViewToStringRenderer Engine { get; set; }
            }

        }

    }
}
