﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeEnrollment.Application.Employee.Queries.Models
{
    public class EmployeePagedListInput
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        public bool GetAll { get; set; }
    }
}
