﻿using ASL.Hrms.SharedKernel.Interfaces;
using System;

namespace LeaveManagement.Core.ApplicationEvents
{
    public class LeaveNotificationEvent : ICommand
    {
        public Guid ManagerId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public double NoOfDays { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime CommandPublished { get; set; }
        public string UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public Boolean IsHalfDayLeave { get; set; }
        public Guid LeaveTypeId { get;  set; }
    }
}