using ASL.Hrms.SharedKernel.Specifications;
using Attendance.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Attendance.Core.Specifications
{
   public class DesignationFilterSpecification : BaseSpecification<Designation>
    {
    
        public DesignationFilterSpecification()

            : base(x => x.IsDeleted == false)
        {

        }
    }
}
