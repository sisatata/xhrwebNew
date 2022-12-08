using System;


namespace CompanySetup.Application.CompanyAddress.Commands
{
    public class CompanyAddressCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
