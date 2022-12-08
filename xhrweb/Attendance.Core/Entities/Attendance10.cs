using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Attendance.Core.Entities
{
    public class Attendance10 : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {

        public Guid? CardId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public string AttendanceYear { get; private set; }
        public DateTime? AttendanceDate { get; private set; }
        public Int16? Punchtype { get; private set; }
        public Boolean OverNightMark { get; private set; }
        public decimal? Latitude { get; private set; }
        public decimal? Longitude { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public ushort ClockTimeType { get; set; }
        public string Remarks { get; set; }
        public string ClockTimeAddress { get; set; }
        public Attendance10(Guid id) : base(id) { }
        private Attendance10() : base(Guid.NewGuid()) { }

        public static Attendance10 Create(


         Guid? cardId,
         Guid? employeeId,
         string attendanceYear,
         DateTime? attendanceDate,
         Int16? punchtype,
         Boolean overNightMark,
         decimal? latitude,
         decimal? longitude,
         Boolean isDeleted,
         ushort clockTimeType,
         string remarks,
         string clockTimeAddress

        )

        {
            var oModel = new Attendance10(Guid.NewGuid());


            oModel.CardId = cardId;
            oModel.EmployeeId = employeeId;
            oModel.AttendanceYear = attendanceYear;
            oModel.AttendanceDate = attendanceDate;
            oModel.Punchtype = punchtype;
            oModel.OverNightMark = overNightMark;
            oModel.Latitude = latitude;
            oModel.Longitude = longitude;
            oModel.IsDeleted = isDeleted;
            oModel.ClockTimeType = clockTimeType;
            oModel.Remarks = remarks;
            oModel.ClockTimeAddress = clockTimeAddress;
            return oModel;

        }


        public void UpdateAttendance10
            (


         Guid? cardId,
         Guid? employeeId,
         string attendanceYear,
         DateTime? attendanceDate,
         Int16? punchtype,
         Boolean overNightMark,
         decimal? latitude,
         decimal? longitude,
         Boolean isDeleted,
         ushort clockTimeType,
         string remarks,
         string clockTimeAddress

        )
        {

            CardId = cardId;
            EmployeeId = employeeId;
            AttendanceYear = attendanceYear;
            AttendanceDate = attendanceDate;
            Punchtype = punchtype;
            OverNightMark = overNightMark;
            Latitude = latitude;
            Longitude = longitude;
            IsDeleted = isDeleted;
            ClockTimeType = clockTimeType;
            Remarks = remarks;
            ClockTimeAddress = clockTimeAddress;
        }


        public void MarkAsDeleteAttendance10()
        {
            IsDeleted = true;
        }


    }
}

