﻿using ASL.Hrms.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Events
{
    public class EmployeeActivityLogEvent : ICommand
    {
        public string SystemKeyword { get; set; }
        public string Comment { get; set; }
        //public BaseEntity<Guid> Entity { get; set; }
        public DateTime CommandPublished { get; set; }
        public string LoginUserId { get; set; }
        public string CurrentUserCompanyId { get; set; }
        public string UserId { get; set; }
    }
}
