using Attendance.Application.ShiftGroup.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftGroup.Queries
{
    public class Queries
    {
        public class GetShiftGroupByCompany : IRequest<List<ShiftGroupVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetShiftGroupById : IRequest<ShiftGroupVM>
        {
            public Guid ShiftGroupId { get; set; }
        }
    }
}
