using System;


namespace EmployeeEnrollment.Application.EmployeeAddress .Commands
{
  public  class EmployeeAddressCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
