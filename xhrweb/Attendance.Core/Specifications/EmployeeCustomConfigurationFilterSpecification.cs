using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
    public class EmployeeCustomConfigurationFilterSpecification : BaseSpecification<EmployeeCustomConfiguration>
    {
        public EmployeeCustomConfigurationFilterSpecification(string code)

            : base(x => x.Code == code)
        {

        }
    }
}