using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Upazila.Queries.Models
{
    public class UpazilaVM
    {
        public Guid Id { get; set; }
        public Guid DistrictId { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameLocalized { get; set; }
    }
}
