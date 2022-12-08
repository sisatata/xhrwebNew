using System;


namespace EmployeeEnrollment.Application.EmployeeEmail .Commands
{
  public  class EmployeeEmailCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
