using DinkToPdf.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Wkhtmltopdf.NetCore;

namespace Attendance.Application.ReportPrint.Commands
{
   public class PrintJobCardSummaryReportCommand : IRequest<string>
    {
        public Guid CompanyId { get; set; }
       
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public IConverter Converter { get; set; }
        public IRazorViewToStringRenderer Engine { get; set; }
        public List<Guid> BranchIds { get; set; }
        public List<Guid> EmployeeIds { get; set; }
        public List<Guid> DepartmentIds { get; set; }
        public List<Guid> DesignationIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
       
    }
}
