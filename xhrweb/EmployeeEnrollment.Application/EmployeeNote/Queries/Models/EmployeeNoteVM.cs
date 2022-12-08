using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeNote.Queries.Models
{
    public class EmployeeNoteVM
    {
        public Guid? Id { get; set; }
        public Guid? EmployeeId { get; set; }
        public string Note { get; set; }
        public Guid? DownloadId { get; set; }
        public Boolean DisplayToEmployee { get; set; }
    }
}
