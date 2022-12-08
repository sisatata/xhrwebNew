using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Core.Entities
{
    public class Shift : BaseEntity<Guid>, IAggregateRoot, IAuditable
	{
		public Guid Id { get; private set; }
		public Guid CompanyId { get; private set; }
		public Guid ShiftGroupID { get; private set; }
		public string ShiftName { get; private set; }		
		public string ShiftLocalizedName { get; private set; }
		public string ShiftCode { get; private set; }
		public bool IsActive { get; private set; }
		public DateTime ShiftIn { get;  set; }
		public DateTime ShiftOut { get;  set; }
		public DateTime ShiftLate { get;  set; }
		public DateTime LunchBreakIn { get;  set; }
		public DateTime  LunchBreakOut { get;  set; }
		public DateTime EarlyOut { get;  set; }
		public DateTime RegHour { get;  set; }
		public DateTime RamadanIn { get;  set; }
		public DateTime RamadanOut { get;  set; }
		public DateTime RamadanLate { get;  set; }
		public DateTime RamadanEarlyOut { get;  set; }
		public DateTime  RamadanLunchBreakIn { get;  set; }
		public DateTime RamadanLunchBreakOut { get;  set; }
		public string  StartTime { get; private set; }
		public string EndTime { get; private set; }
		public int GraceIn { get; private set; }
		public int  GraceOut { get; private set; }
		public int  Range { get; private set; }
		public bool IsRollingShift { get; private set; }
		public int RollingSequence { get; private set; }
		public bool  IsRelaborShift { get; private set; }		
		public bool  IsDeleted  { get; private set; }
        public DateTime StartRange { get; set; }
		public DateTime EndRange { get; set; }
		public Shift(Guid id) : base(id) { }
		private Shift() : base(Guid.NewGuid()) { }

		

		public static Shift ShiftConfiguration(Guid companyId, Guid shiftGroupID, string shiftName, string shiftLocalizedName, string shiftCode, 
			bool isActive, DateTime shiftIn, DateTime shiftOut, DateTime shiftLate, DateTime lunchBreakIn, DateTime lunchBreakOut, DateTime earlyOut,
			DateTime regHour, DateTime ramadanIn, DateTime ramadanOut, DateTime ramadanLate, DateTime ramadanEarlyOut,
			DateTime ramadanLunchBreakIn, DateTime ramadanLunchBreakOut, string startTime, string endTime, int graceIn, int graceOut,
			int range, bool isRollingShift, bool isRelaborShift, bool isDeleted)
		{
			Guard.Against.NullOrEmptyGuid(companyId, "companyId");
			Guard.Against.NullOrWhiteSpace(shiftName, "shiftName");
			Guard.Against.NullOrWhiteSpace(shiftCode, "shiftCode");
			var shift = new Shift(Guid.NewGuid())
			{
			CompanyId = companyId,
			ShiftGroupID = shiftGroupID,
			ShiftName = shiftName,
			ShiftLocalizedName = shiftLocalizedName,
			ShiftCode = shiftCode,
			IsActive = isActive,
			ShiftIn = shiftIn,
			ShiftOut = shiftOut,
			ShiftLate = shiftLate,
			LunchBreakIn = lunchBreakIn,
			LunchBreakOut = lunchBreakOut,
			EarlyOut = earlyOut,
			RegHour = regHour,
			RamadanIn = ramadanIn,
			RamadanOut = ramadanOut,
			RamadanLate = ramadanLate,
			RamadanEarlyOut = ramadanEarlyOut,
			RamadanLunchBreakIn = ramadanLunchBreakIn,
			RamadanLunchBreakOut = ramadanLunchBreakOut,
			StartTime = startTime,
			EndTime = endTime,
			GraceIn = graceIn,
			GraceOut = graceOut,
			Range = range,
			IsRollingShift = isRollingShift,
			IsRelaborShift = isRelaborShift,
			IsDeleted = isDeleted
		};
		return shift;	
		}
		public void UpdateShift(Guid companyId, Guid shiftGroupID, string shiftName, string shiftLocalizedName, string shiftCode, bool isActive, DateTime shiftIn,
			DateTime shiftOut, DateTime shiftLate, DateTime lunchBreakIn, DateTime lunchBreakOut, DateTime earlyOut, DateTime regHour,
			DateTime ramadanIn, DateTime ramadanOut, DateTime ramadanLate, DateTime ramadanEarlyOut, DateTime ramadanLunchBreakIn,
			DateTime ramadanLunchBreakOut, string startTime, string endTime, int graceIn, int graceOut, int range, bool isRollingShift,
			bool isRelaborShift, bool isDeleted)
		{
			Guard.Against.NullOrEmptyGuid(companyId, "companyId");
			Guard.Against.NullOrWhiteSpace(shiftName, "shiftName");
			Guard.Against.NullOrWhiteSpace(shiftCode, "shiftCode");

			CompanyId = companyId;
			ShiftGroupID = shiftGroupID;
			ShiftName = shiftName;
			ShiftLocalizedName = shiftLocalizedName;
			ShiftCode = shiftCode;
			IsActive = isActive;
			ShiftIn = shiftIn;
			ShiftOut = shiftOut;
			ShiftLate = shiftLate;
			LunchBreakIn = lunchBreakIn;
			LunchBreakOut = lunchBreakOut;
			EarlyOut = earlyOut;
			RegHour = regHour;
			RamadanIn = ramadanIn;
			RamadanOut = ramadanOut;
			RamadanLate = ramadanLate;
			RamadanEarlyOut = ramadanEarlyOut;
			RamadanLunchBreakIn = ramadanLunchBreakIn;
			RamadanLunchBreakOut = ramadanLunchBreakOut;
			StartTime = startTime;
			EndTime = endTime;
			GraceIn = graceIn;
			GraceOut = graceOut;
			Range = range;
			IsRollingShift = isRollingShift;
			IsRelaborShift = isRelaborShift;
			IsDeleted = isDeleted;
		}
		public void MarkShiftAsDeleted()
		{
			IsDeleted = true;
		}
	}
}
