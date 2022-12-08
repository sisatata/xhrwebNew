using DinkToPdf.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Wkhtmltopdf.NetCore;
namespace EmployeeEnrollment.Application.ReportPrint.Commands
{
   public static class Commands
    {
        public static class V1
        {
            public class PrintEmployeeEnrollmentReport : IRequest<string>
            {
                public Guid CompanyId { get; set; }

                public Guid? EmployeeId { get; set; }

                public Int32? Type { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public IConverter Converter { get; set; }
                public IRazorViewToStringRenderer Engine { get; set; }
            }
            public class PrintEmployeeConfirmationReport : IRequest<string>
            {
                public Guid CompanyId { get; set; }

                public Guid? EmployeeId { get; set; }
                public DateTime? StartDate { get; set; }
                public DateTime? EndDate { get; set; }
                public IConverter Converter { get; set; }
                public IRazorViewToStringRenderer Engine { get; set; }
            }

        }
    }
}
