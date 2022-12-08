﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Shift.Queries.Models
{
    public class ShiftVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { set; get; }
        public Guid ShiftGroupId { get; set; }
        public string ShiftGroupName { get; set; }
        public string ShiftName { get; set; }
        public string ShiftLocalizedName { get; set; }
        public string ShiftCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime ShiftIn { get; set; }
        public DateTime ShiftOut { get; set; }
        public DateTime ShiftLate { get; set; }
        public DateTime LunchBreakIn { get; set; }
        public DateTime LunchBreakOut { get; set; }
        public DateTime EarlyOut { get; set; }
        public DateTime RegHour { get; set; }
        public DateTime RamadanIn { get; set; }
        public DateTime RamadanOut { get; set; }
        public DateTime RamadanLate { get; set; }
        public DateTime RamadanEarlyOut { get; set; }
        public DateTime RamadanLunchBreakIn { get; set; }
        public DateTime RamadanLunchBreakOut { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int GraceIn { get; set; }
        public int GraceOut { get; set; }
        public int Range { get; set; }
        public bool IsRollingShift { get; set; }
        public bool IsRelaborShift { get; set; }
        public bool IsDeleted { get; set; }
    }
}
