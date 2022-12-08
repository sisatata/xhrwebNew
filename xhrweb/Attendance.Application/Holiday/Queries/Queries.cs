using Attendance.Application.Holiday.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace Attendance.Application.Holiday.Queries
{
    public static class Queries
    {
        public class GetHolidayList : IRequest<List<HolidayVM>>
        {
            public Guid CompanyId { get; set; }
        }
        public class GetHolidayListByFinancialYear : IRequest<List<HolidayVM>>
        {
            public Guid CompanyId { get; set; }

            public string Financialyear { get; set; }
        }

        public class GetHoliday : IRequest<HolidayVM>
        {
            public Guid Id { get; set; }
        }
    }
}
