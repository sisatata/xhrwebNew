using Ardalis.GuardClauses;
using System;

namespace ASL.Hrms.SharedKernel.ExtensionMethods
{
    public static class GuardExtension
    {
        public static void NullOrEmptyGuid(this IGuardClause guardClause, Guid input, string parameterName)
        {
            if(input == null || input == Guid.Empty)
            {
                throw new ArgumentException("Should not have null or empty", parameterName);
            }
        }
    }
}
