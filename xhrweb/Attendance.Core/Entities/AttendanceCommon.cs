using ASL.Hrms.SharedKernel;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Attendance.Core.Entities
{
    public class AttendanceCommon : BaseEntity<Guid>, IAggregateRoot
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

        public AttendanceCommon(Guid id) : base(id) { }
        private AttendanceCommon() : base(Guid.NewGuid()) { }

        public static AttendanceCommon Create(


         Guid? cardId,
         Guid? employeeId,
         string attendanceYear,
         DateTime? attendanceDate,
         Int16? punchtype,
         Boolean overNightMark,
         decimal? latitude,
         decimal? longitude,
         Boolean isDeleted

        )

        {
            var oModel = new AttendanceCommon(Guid.NewGuid());


            oModel.CardId = cardId;
            oModel.EmployeeId = employeeId;
            oModel.AttendanceYear = attendanceYear;
            oModel.AttendanceDate = attendanceDate;
            oModel.Punchtype = punchtype;
            oModel.OverNightMark = overNightMark;
            oModel.Latitude = latitude;
            oModel.Longitude = longitude;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateAttendance
            (


         Guid? cardId,
         Guid? employeeId,
         string attendanceYear,
         DateTime? attendanceDate,
         Int16? punchtype,
         Boolean overNightMark,
         decimal? latitude,
         decimal? longitude,
         Boolean isDeleted

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
        }


        public void MarkAsDeleteAttendance()
        {
            IsDeleted = true;
        }


    }
}

