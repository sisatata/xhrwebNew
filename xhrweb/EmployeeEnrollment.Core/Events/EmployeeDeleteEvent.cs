using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Events
{
    public class EmployeeDeleteEvent : ICommand
    {
        public Guid LoginId { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }
    }
}
