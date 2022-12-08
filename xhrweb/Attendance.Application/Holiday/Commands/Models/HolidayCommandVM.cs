using System;


namespace Attendance.Application.Holiday .Commands
{
  public  class HolidayCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
