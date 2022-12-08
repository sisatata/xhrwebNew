using DinkToPdf.Contracts;
using MediatR;
using System;
using Wkhtmltopdf.NetCore;
namespace LeaveManagement.Application.ReportPrint.Commands
{
   public class PrintLeaveApplicationReportCommand : IRequest<string>
    {
        public Guid CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EmployeeName { get; set; }

        public string LeaveTypeName { get; set; }
        public string ApprovalStatusText { get; set; }
        public IConverter Converter { get; set; }
        public IRazorViewToStringRenderer Engine { get; set; }
    }
}
