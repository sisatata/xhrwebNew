using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Branch.Commands.Models
{
    public class BranchCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
