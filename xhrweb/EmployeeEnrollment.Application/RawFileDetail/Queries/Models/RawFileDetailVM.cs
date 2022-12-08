using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.RawFileDetail.Queries.Models
{
    public class RawFileDetailVM
    {
        public Guid? Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Comments { get; set; }
        public Guid? CompanyId { get; set; }
        public string Status { get; set; }
        public int TotalFail { get; set; }
        public int TotalRecord { get; set; }
        public int TotalSuccess { get; set; }
    }
}
