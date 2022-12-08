using System;


namespace PayrollManagement.Application.PaymentOption .Commands
{
  public  class PaymentOptionCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
