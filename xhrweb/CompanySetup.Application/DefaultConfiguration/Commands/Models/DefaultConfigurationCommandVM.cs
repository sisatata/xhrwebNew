using System;


namespace CompanySetup.Application.DefaultConfiguration.Commands
{
    public class DefaultConfigurationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
