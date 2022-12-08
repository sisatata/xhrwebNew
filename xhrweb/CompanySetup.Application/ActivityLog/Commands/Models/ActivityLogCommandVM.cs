using System;


namespace CompanySetup.Application.ActivityLog.Commands
{
    public class ActivityLogCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
