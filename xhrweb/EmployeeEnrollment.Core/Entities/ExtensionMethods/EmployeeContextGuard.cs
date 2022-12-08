using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeEnrollment.Core.Entities.ExtensionMethods
{
    public static class EmployeeContextGuard
    {
        public static void DuplicateEmployeeId(this IGuardClause guardClause, string input, IReadOnlyList<Employee> checkWithList,
            string parameterName, string message = null)
        {
            if (checkWithList != null && checkWithList.Count > 0)
            {
                var duplicateRecord = checkWithList.FirstOrDefault(r => r.EmployeeId != null && r.EmployeeId.ToLower().Trim() == input.ToLower().Trim());
                if (duplicateRecord != null)
                {
                    throw new ArgumentException(message ?? "EmployeeId duplicated. Please choose another!", parameterName);
                }
            }
        }

        public static void DuplicateEmployeeIdUpdateMode(this IGuardClause guardClause, Guid id, string input, IReadOnlyList<Employee> checkWithList,
           string parameterName, string message = null)
        {
            if (checkWithList != null && checkWithList.Count > 0)
            {
                var duplicateRecord = checkWithList.FirstOrDefault(r => r.EmployeeId != null && r.EmployeeId.ToLower().Trim() == input.ToLower().Trim() && r.Id != id);
                if (duplicateRecord != null)
                {
                    throw new ArgumentException(message ?? "EmployeeId duplicated. Please choose another!", parameterName);
                }
            }
        }
    }
}
