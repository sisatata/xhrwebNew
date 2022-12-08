using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BankBranch.Queries.Models
{
    public class BankBranchVM
    {
        public Guid? Id { get; set; }
        public string BranchName { get; set; }
        public string BranchNameLC { get; set; }
        public string BranchAddress { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmailId { get; set; }
        public Int32? SortOrder { get; set; }
        public Guid? CompanyId { get; set; }
        public Boolean IsDeleted { get; set; }
        public Guid BankId { get; set; }
    }
}
