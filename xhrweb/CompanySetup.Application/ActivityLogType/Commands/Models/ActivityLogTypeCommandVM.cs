using System;


namespace CompanySetup.Application.ActivityLogType .Commands
{
  public  class ActivityLogTypeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
