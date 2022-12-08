using DinkToPdf.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Wkhtmltopdf.NetCore;

namespace PayrollManagement.Application.ReportPrint.Commands
{
   public class PrintSalaryReportCommand : IRequest<string>
    {
        public Guid CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }

        public Guid FinancialYearId { get; set; }
        public Guid MonthCycleId { get; set; }
        public IConverter Converter { get; set; }
        public IRazorViewToStringRenderer Engine { get; set; }

       
    }
}
