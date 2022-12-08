using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.AttendanceRequest.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateAttendanceRequest : IRequest<AttendanceRequestCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public int RequestTypeId { get; set; }
                public DateTime? StartTime { get; set; }
                public DateTime? EndTime { get; set; }
                public string Reason { get; set; }
                //public string AprovalStatus { get; set; }
                //public string Note { get; set; }
                //public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class UpdateAttendanceRequest : IRequest<AttendanceRequestCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid EmployeeId { get; set; }
                public int RequestTypeId { get; set; }
                public DateTime? StartTime { get; set; }
                public DateTime? EndTime { get; set; }
                public string Reason { get; set; }
                //public string AprovalStatus { get; set; }
                //public string Note { get; set; }
                //public Boolean IsDeleted { get; set; }
                public Guid CompanyId { get; set; }
            }

            public class MarkAsDeleteAttendanceRequest : IRequest<AttendanceRequestCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class ApproveAttendanceRequest : IRequest<AttendanceRequestCommandVM>
            {
                public Guid Id { get; set; }
                //public Guid EmployeeId { get; set; }
                //public int RequestTypeId { get; set; }
                //public DateTime? StartTime { get; set; }
                //public DateTime? EndTime { get; set; }
                //public string Reason { get; set; }
                //public string AprovalStatus { get; set; }
                public string Note { get; set; }
                //public Boolean IsDeleted { get; set; }
                //public Guid CompanyId { get; set; }
            }

            public class RejectAttendanceRequest : IRequest<AttendanceRequestCommandVM>
            {
                public Guid Id { get; set; }
                //public Guid EmployeeId { get; set; }
                //public int RequestTypeId { get; set; }
                //public DateTime? StartTime { get; set; }
                //public DateTime? EndTime { get; set; }
                //public string Reason { get; set; }
                //public string AprovalStatus { get; set; }
                public string Note { get; set; }
                //public Boolean IsDeleted { get; set; }
                //public Guid CompanyId { get; set; }
            }
        }
    }
}
