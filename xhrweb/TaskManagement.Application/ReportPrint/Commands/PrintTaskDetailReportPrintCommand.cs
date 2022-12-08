using DinkToPdf.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Wkhtmltopdf.NetCore;
namespace TaskManagement.Application.ReportPrint.Commands
{
   public class PrintTaskDetailReportPrintCommand : IRequest<string>
    {
        public Guid CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? ParentTaskId { get; set; }
        public IConverter Converter { get; set; }
        public IRazorViewToStringRenderer Engine { get; set; }
        public Guid? UserId { get; set; }

    }
}
