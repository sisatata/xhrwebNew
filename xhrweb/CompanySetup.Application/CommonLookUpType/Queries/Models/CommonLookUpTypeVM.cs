using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.CommonLookUpType.Queries.Models
{
    public class CommonLookUpTypeVM
    {
        public Guid Id { get; set; }
        public string ShortCode { get; set; }
        public string LookUpTypeName { get; set; }
        public string LookUpTypeNameLC { get; set; }
        public string Remarks { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? ParentId { get; set; }
        public Int16? SortOrder { get; set; }
    }
}
