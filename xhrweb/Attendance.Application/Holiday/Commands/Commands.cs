using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Holiday.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateHoliday : IRequest<HolidayCommandVM>
            {
                //public Guid? Id { get; set; }
                public DateTime HolidayDate { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public string Reason { get; set; }
                public string ReasonLocalized { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateHoliday : IRequest<HolidayCommandVM>
            {
                public Guid Id { get; set; }
                public DateTime HolidayDate { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public string Reason { get; set; }
                public string ReasonLocalized { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteHoliday : IRequest<HolidayCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
