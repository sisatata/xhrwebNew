using ASL.Hrms.SharedKernel.Specifications;
using LeaveManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.Core.Specifications
{
   public class DesignationFilterSpecification : BaseSpecification<Designation>
    {
        public DesignationFilterSpecification()

           : base(x => x.IsDeleted == false)
        {

        }
    }
}
