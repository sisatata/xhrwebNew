using Ardalis.GuardClauses;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanySetup.Core.ExtensionMethods
{
    public static class CompanyContextGuard
    {
        public static void InvalidCompanyStatus(this IGuardClause guardClause, string input, string checkWith,
            string parameterName, string message = null)
        {
            if (input == checkWith)
            {
                throw new ArgumentException(message ?? "Not a valid status", parameterName);
            }
        }

        public static void DuplicateCompanyName(this IGuardClause guardClause, string input, IReadOnlyList<Company> checkWithList,
            string parameterName, string message = null)
        {
            if (checkWithList != null && checkWithList.Count > 0)
            {
                var duplicateRecord = checkWithList.FirstOrDefault(r => r.CompanyName.ToLower().Trim() == input.ToLower().Trim());
                if (duplicateRecord != null)
                {
                    throw new ArgumentException(message ?? "Company duplicated. Please choose another!", parameterName);
                }
            }
        }
        public static void DuplicateCompanyNameUpdateMode(this IGuardClause guardClause, Guid id, string input, IReadOnlyList<Company> checkWithList,
           string parameterName, string message = null)
        {
            if (checkWithList != null && checkWithList.Count > 0)
            {
                var duplicateRecord = checkWithList.FirstOrDefault(r => r.CompanyName.ToLower().Trim() == input.ToLower().Trim() && r.Id != id);
                if (duplicateRecord != null)
                {
                    throw new ArgumentException(message ?? "Company name duplicated. Please choose another!", parameterName);
                }
            }
        }
    }
}
