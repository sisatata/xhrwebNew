using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.ExtensionMethods
{
    public static class AttendanceContextGuard
    {
        public static void DuplicateEntry(this IGuardClause guardClause, object obj,
             string message = null)
        {
            if (obj != null)
            {
                throw new ArgumentException("Shift already allocated for the employee");
            }
        }
    }
}
