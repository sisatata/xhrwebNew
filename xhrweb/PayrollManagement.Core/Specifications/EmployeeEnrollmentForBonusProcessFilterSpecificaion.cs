using ASL.Hrms.SharedKernel.Specifications;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Core.Specifications
{
    public class EmployeeEnrollmentForBonusProcessFilterSpecificaion : BaseSpecification<Employee>

    {
        public EmployeeEnrollmentForBonusProcessFilterSpecificaion(Guid companyId, Guid? employeeId, DateTime bonusProcessDate)
            : base(x => x.CompanyId == companyId && x.JoiningDate < bonusProcessDate &&
            (x.QuitDate == null || x.QuitDate > bonusProcessDate.Date) && (employeeId == null || employeeId == Guid.Empty || x.Id == employeeId))
        {
        }
    }
}
