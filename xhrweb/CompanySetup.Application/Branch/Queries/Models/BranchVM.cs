using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Branch.Queries.Models
{
    public class BranchVM
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string ShortName { get; set; }
        public string BranchLocalizedName { get; set; }
        public uint SortOrder { get; set; }
    }
}
