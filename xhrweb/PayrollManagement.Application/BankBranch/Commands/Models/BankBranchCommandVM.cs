using System;


namespace PayrollManagement.Application.BankBranch .Commands
{
  public  class BankBranchCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
