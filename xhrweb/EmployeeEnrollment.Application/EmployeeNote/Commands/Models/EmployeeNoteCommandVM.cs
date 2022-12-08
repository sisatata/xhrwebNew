using System;


namespace EmployeeEnrollment.Application.EmployeeNote .Commands
{
  public  class EmployeeNoteCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
