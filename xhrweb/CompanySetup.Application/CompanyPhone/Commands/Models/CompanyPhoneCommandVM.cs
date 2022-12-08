using System;


namespace CompanySetup.Application.CompanyPhone .Commands
{
  public  class CompanyPhoneCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
