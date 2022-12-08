using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Events
{
    public class ApproveCompanyEvent : ICommand
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }

        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
