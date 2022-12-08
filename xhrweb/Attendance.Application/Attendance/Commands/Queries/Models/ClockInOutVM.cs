using ASL.Hrms.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Application.Attendance.Queries.Models
{
    public class ClockInOutVM
    {
        private Int16 _punchtype;
        private decimal? _clockTimeType;

        public Guid? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public Guid PositionId { get; set; }
        public Guid DepartmentId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string PunchtypeText { get; set; }
        public Int16 Punchtype
        {
            get { return this._punchtype; }
            set
            {
                this._punchtype = value;
                PunchtypeText = ((PunchTypes)this._punchtype).ToString();
            }
        }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string ClockTimeAddress { get; set; }
        public string ClockTimeTypeText { get; set; }
        public decimal? ClockTimeType
        {
            get { return this._clockTimeType; }
            set
            {
                this._clockTimeType = value;
                ClockTimeTypeText = ((ClockTypes)this._clockTimeType).ToString();
            }
        }
        public string Remarks { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Status { get; set; }
        public string ShiftCode { get; set; }
        public DateTime ShiftIn { get; set; }
        public DateTime ShiftOut { get; set; }
    }
}
