﻿using ASL.Hrms.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagement.Core.Entities
{
    public class EmployeeManager : BaseEntity<Guid>
    {
        public Guid EmployeeId { get; private set; }
        public Guid? ManagerId { get; private set; }
        public Boolean IsPrimaryManager { get; private set; }
        public Guid? AssignedBy { get; private set; }
        public DateTime? AssignDate { get; private set; }
        public Guid? UnAssignedBy { get; private set; }
        public DateTime? UnAssignDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid? ManagerTypeId { get; private set; }


        public EmployeeManager(Guid id) : base(id) { }
        private EmployeeManager() : base(Guid.NewGuid()) { }
    }
}
