using Attendance.Core.Entities.AttendanceProcessAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Interfaces
{
   public interface IAttendanceProcessDataRepository
    {
        Task Update(AttendanceProcessAggregate attendanceProcessData);
    }
}
