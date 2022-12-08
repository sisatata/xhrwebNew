using Attendance.Application.ShiftGroup.Commands.Models;
using Attendance.Application.ShiftGroup.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftGroup.Commands
{
    public class ShiftGroupCommands
    {
        public static class V1
        {
            public class CreateShiftGroup : IRequest<ShiftGroupCommandVM>
            {
                public Guid CompanyId { get; set; }
                public string ShiftGroupName { get; set; }
                public string ShiftGroupNameLC { get; set; }
            }

            public class UpdateShiftGroup : IRequest<ShiftGroupCommandVM>
            {
                public Guid Id { get; set; }
                public Guid CompanyId { get; set; }
                public string ShiftGroupName { get; set; }
                public string ShiftGroupNameLC { get; set; }
            }

            public class MarkShiftGroupAsDelete : IRequest<ShiftGroupCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
