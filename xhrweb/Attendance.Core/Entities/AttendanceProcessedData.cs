using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Attendance.Core.Entities
{
    public class AttendanceProcessedData : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? EmployeeId { get; private set; }
        public DateTime PunchDate { get; private set; }
        public string PunchYear { get; private set; }
        public Int32? PunchMonth { get; private set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? ShiftIn { get; set; }
        public DateTime? ShiftOut { get; set; }
        public DateTime? BreakIn { get; set; }
        public DateTime? BreakOut { get; set; }
        public DateTime? BreakLate { get; set; }
        public DateTime? Late { get; set; }
        public Guid? ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public DateTime? RegularHour { get; set; }
        public DateTime? OTHour { get; set; }
        public string Status { get; set; }
        public string Status_V2 { get; set; }
        public DateTime? BuyerShiftIn { get; set; }
        public DateTime? BuyerShiftOut { get; set; }
        public DateTime? BuyerOTTime { get; set; }
        public string Remarks { get; set; }
        public Boolean IsLunchOut { get; set; }
        public Guid? FinancialYearId { get; set; }
        public Guid? MonthCycleId { get; set; }

        public decimal? TimeOutLatitude { get; set; }
        public decimal? TimeOutLongitude { get; set; }

        public decimal? TimeInLatitude { get; set; }
        public decimal? TimeInLongitude { get; set; }

        public string EmployeeRemarks { get; set; }
        public string ClockInAddress { get; set; }
        public string ClockOutAddress { get; set; }
        // not persisted
        public TrackingState State { get; set; }
        public AttendanceProcessedData(Guid id) : base(id) { }
        private AttendanceProcessedData() : base(Guid.NewGuid()) { }

        public static AttendanceProcessedData Create(

         Guid? employeeId,
         DateTime punchDate,
         string punchYear,
         Int32? punchMonth,
         DateTime? timeIn,
         DateTime? timeOut,
         DateTime? shiftIn,
         DateTime? shiftOut,
         DateTime? breakIn,
         DateTime? breakOut,
         DateTime? breakLate,
         DateTime? late,
         Guid? shiftId,
         string shiftCode,
         DateTime? regularHour,
         DateTime? oTHour,
         string status,
         string status_V2,
         DateTime? buyerShiftIn,
         DateTime? buyerShiftOut,
         DateTime? buyerOTTime,
         string remarks,
         Boolean isLunchOut,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? timeInLatitude,
         decimal? timeInLongitude,
         decimal? timeOutLatitude,
         decimal? timeOutLongitude,
         string employeeRemarks,
         string clockInAddress,
         string clockOutAddress
        )

        {
            var oModel = new AttendanceProcessedData(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.PunchDate = punchDate;
            oModel.PunchYear = punchYear;
            oModel.PunchMonth = punchMonth;
            oModel.TimeIn = timeIn;
            oModel.TimeOut = timeOut;
            oModel.ShiftIn = shiftIn;
            oModel.ShiftOut = shiftOut;
            oModel.BreakIn = breakIn;
            oModel.BreakOut = breakOut;
            oModel.BreakLate = breakLate;
            oModel.Late = late;
            oModel.ShiftId = shiftId;
            oModel.ShiftCode = shiftCode;
            oModel.RegularHour = regularHour;
            oModel.OTHour = oTHour;
            oModel.Status = status;
            oModel.Status_V2 = status_V2;
            oModel.BuyerShiftIn = buyerShiftIn;
            oModel.BuyerShiftOut = buyerShiftOut;
            oModel.BuyerOTTime = buyerOTTime;
            oModel.Remarks = remarks;
            oModel.IsLunchOut = isLunchOut;
            oModel.FinancialYearId = financialYearId;
            oModel.MonthCycleId = monthCycleId;
            oModel.TimeInLatitude = timeInLatitude;
            oModel.TimeInLongitude = timeInLongitude;

            oModel.TimeOutLatitude = timeOutLatitude;
            oModel.TimeOutLongitude = timeOutLongitude;

            oModel.EmployeeRemarks = employeeRemarks;
            oModel.ClockInAddress = clockInAddress;
            oModel.ClockOutAddress = clockOutAddress;
            return oModel;

        }


        public void UpdateAttendanceProcessedData
            (

         Guid? employeeId,
         DateTime punchDate,
         string punchYear,
         Int32? punchMonth,
         DateTime? timeIn,
         DateTime? timeOut,
         DateTime? shiftIn,
         DateTime? shiftOut,
         DateTime? breakIn,
         DateTime? breakOut,
         DateTime? breakLate,
         DateTime? late,
         Guid? shiftId,
         string shiftCode,
         DateTime? regularHour,
         DateTime? oTHour,
          string status,
         string status_V2,
         DateTime? buyerShiftIn,
         DateTime? buyerShiftOut,
         DateTime? buyerOTTime,
         string remarks,
         Boolean isLunchOut,
         Guid? financialYearId,
         Guid? monthCycleId,
         decimal? timeInLatitude,
         decimal? timeInLongitude,
         decimal? timeOutLatitude,
         decimal? timeOutLongitude,
         string employeeRemarks,
         string clockInAddress,
         string clockOutAddress
        )
        {
            EmployeeId = employeeId;
            PunchDate = punchDate;
            PunchYear = punchYear;
            PunchMonth = punchMonth;
            TimeIn = timeIn;
            TimeOut = timeOut;
            ShiftIn = shiftIn;
            ShiftOut = shiftOut;
            BreakIn = breakIn;
            BreakOut = breakOut;
            BreakLate = breakLate;
            Late = late;
            ShiftId = shiftId;
            ShiftCode = shiftCode;
            RegularHour = regularHour;
            OTHour = oTHour;
            Status = status;
            Status_V2 = status_V2;
            BuyerShiftIn = buyerShiftIn;
            BuyerShiftOut = buyerShiftOut;
            BuyerOTTime = buyerOTTime;
            Remarks = remarks;
            IsLunchOut = isLunchOut;
            FinancialYearId = financialYearId;
            MonthCycleId = monthCycleId;
            TimeInLatitude = TimeInLatitude;
            TimeInLongitude = timeInLongitude;
            TimeOutLatitude = TimeOutLatitude;
            TimeOutLongitude = timeOutLongitude;
            EmployeeRemarks = employeeRemarks;
            ClockInAddress = clockInAddress;
            ClockOutAddress = clockOutAddress;
        }


        public void MarkAsDeleteAttendanceProcessedData()
        {
            //IsDeleted = true;
        }


    }
}

