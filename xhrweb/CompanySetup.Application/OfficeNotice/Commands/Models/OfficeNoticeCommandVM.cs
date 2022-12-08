using System;


namespace CompanySetup.Application.OfficeNotice.Commands
{
    public class OfficeNoticeCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
