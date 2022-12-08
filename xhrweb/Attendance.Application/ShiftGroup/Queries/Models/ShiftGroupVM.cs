using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Application.ShiftGroup.Queries.Models
{
    public class ShiftGroupVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ShiftGroupName { get; set; }
        public string ShiftGroupNameLC { get; set; }
    }
}
