using ASL.Hrms.SharedKernel.Enums;
using Attendance.Core.Entities.AttendanceProcessAggregate;
using Attendance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Persistence.Repositories
{
    public class AttendanceProcessDataRepository : IAttendanceProcessDataRepository
    {
        private readonly AttendanceContext _attendanceContext;

        public AttendanceProcessDataRepository(AttendanceContext attendanceContext)
        {
            _attendanceContext = attendanceContext;
        }
        public async Task Update(AttendanceProcessAggregate attendanceProcessData)
        {
            foreach (var Item in attendanceProcessData.attendanceProcessedDatas)
            {
                if (Item.State == TrackingState.Added)
                {
                    _attendanceContext.Entry(Item).State = EntityState.Added;
                }
                if (Item.State == TrackingState.Modified)
                {
                    _attendanceContext.Entry(Item).State = EntityState.Modified;
                }
                if (Item.State == TrackingState.Deleted)
                {
                    _attendanceContext.Entry(Item).State = EntityState.Deleted;
                }
            }
            await _attendanceContext.SaveChangesAsync();
        }
    }
}
