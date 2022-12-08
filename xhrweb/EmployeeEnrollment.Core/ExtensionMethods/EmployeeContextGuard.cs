using Ardalis.GuardClauses;
using EmployeeEnrollment.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeEnrollment.Core.ExtensionMethods
{
    public static class EmployeeContextGuard
    {
        public static void DuplicateRecords(this IGuardClause guardClause, List<EmployeePromotionTransfer> pList, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => (x.StartDate >= startDate && x.StartDate <= endDate) || (x.EndDate >= startDate && x.EndDate <= endDate));

            if (oModel != null)
            {
                var v = $"Employee promotion transfer date duplicated with {oModel.StartDate.ToString("dd/MM/yyyy")} and {oModel.EndDate.Value.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void DuplicateRecordInUpdate(this IGuardClause guardClause, List<EmployeePromotionTransfer> pList, Guid Id, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => x.Id != Id && ((x.StartDate >= startDate && x.StartDate <= endDate) || (x.EndDate >= startDate && x.EndDate <= endDate)));

            if (oModel != null)
            {
                var v = $"Employee promotion transfer date duplicated with {oModel.StartDate.ToString("dd/MM/yyyy")} and {oModel.EndDate.Value.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void InvalidPromotionStatus(this IGuardClause guardClause, string input, string checkWith,
            string parameterName, string message = null)
        {
            if (input == checkWith)
            {
                throw new ArgumentException(message ?? "Not a valid status", parameterName);
            }
        }
    }
}
