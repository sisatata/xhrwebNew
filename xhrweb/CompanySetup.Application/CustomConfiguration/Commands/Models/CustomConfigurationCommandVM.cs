using System;


namespace CompanySetup.Application.CustomConfiguration .Commands
{
  public  class CustomConfigurationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
