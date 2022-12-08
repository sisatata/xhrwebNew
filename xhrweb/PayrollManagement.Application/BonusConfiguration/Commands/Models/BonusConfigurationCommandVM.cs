using System;


namespace PayrollManagement.Application.BonusConfiguration .Commands
{
  public  class BonusConfigurationCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
