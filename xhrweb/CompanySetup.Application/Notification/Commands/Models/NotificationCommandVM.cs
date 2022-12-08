using System;


namespace CompanySetup.Application.Notification .Commands
{
  public  class NotificationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
