using System;


namespace CompanySetup.Application.CompanyEmail .Commands
{
  public  class CompanyEmailCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
