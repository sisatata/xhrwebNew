using Attendance.Application.Shift.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Shift.Queries
{
   public  class Queries
    {
        public class GetShiftByCompany : IRequest<List<ShiftVM>>
        {
            public Guid CompanyId { get; set; }
            
        }

        public class GetShiftById : IRequest<ShiftVM>
        {
            public Guid ShiftId { get; set; }
        }
    }
}
