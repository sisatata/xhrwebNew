using Ardalis.GuardClauses;
using PayrollManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PayrollManagement.Core.Entities.BonusConfigurationAggregate;

namespace PayrollManagement.Core.ExtensionMethods
{
    public static class PayrollContextGuard
    {
        public static void DuplicateRecords(this IGuardClause guardClause, List<BonusConfiguration> pList, DateTime startDate, DateTime endDate,
            Int32 RangeFromInMonth,
            Int32 RangeToInMonth,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => (x.StartDate >= startDate && x.StartDate <= endDate &&
                (x.RangeFromInMonth >= RangeFromInMonth && x.RangeFromInMonth <= RangeToInMonth) ||
                (x.RangeToInMonth >= RangeFromInMonth && x.RangeToInMonth <= RangeToInMonth))
           || ((x.EndDate >= startDate && x.EndDate <= endDate) &&
                (x.RangeFromInMonth >= RangeFromInMonth && x.RangeFromInMonth <= RangeToInMonth) ||
                (x.RangeToInMonth >= RangeFromInMonth && x.RangeToInMonth <= RangeToInMonth)));

            if (oModel != null)
            {
                var v = $"Bonus Configuration date duplicated with {oModel.StartDate.Value.ToString("dd/MM/yyyy")} and {oModel.EndDate.Value.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void DuplicateRecordInUpdate(this IGuardClause guardClause, List<BonusConfiguration> pList, Guid Id, DateTime startDate, DateTime endDate,
            Int32 RangeFromInMonth,
            Int32 RangeToInMonth,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => x.Id != Id && ((x.StartDate >= startDate && x.StartDate <= endDate &&
           (x.RangeFromInMonth >= RangeFromInMonth && x.RangeFromInMonth <= RangeToInMonth) ||
           (x.RangeToInMonth >= RangeFromInMonth && x.RangeToInMonth <= RangeToInMonth))

           || ((x.EndDate >= startDate && x.EndDate <= endDate) &&
           ((x.RangeFromInMonth >= RangeFromInMonth && x.RangeFromInMonth <= RangeToInMonth) ||
           (x.RangeToInMonth >= RangeFromInMonth && x.RangeToInMonth <= RangeToInMonth)))));

            if (oModel != null)
            {
                var v = $"Bonus Configuration date duplicated with {oModel.StartDate.Value.ToString("dd/MM/yyyy")} and {oModel.EndDate.Value.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void InvalidStartDateEndDate(this IGuardClause guardClause, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException(message ?? "Not a valid start & end date", parameterName);
            }
        }

        public static void InSufficientGross(this IGuardClause guardClause, decimal input, decimal checkWith,
            string parameterName, string message = null)
        {
            if (input <= checkWith)
            {
                throw new ArgumentException(message ?? "In Sufficient Gross amount", parameterName);
            }
        }

        public static void DuplicateRecords(this IGuardClause guardClause, List<EmployeeSalary> pList, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => (x.StartDate >= startDate && x.StartDate <= endDate) || (x.EndDate >= startDate && x.EndDate <= endDate));

            if (oModel != null)
            {
                var v = $"Date overlapped with {oModel.StartDate.Value.ToString("dd/MM/yyyy")} and {oModel.EndDate.Value.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void DuplicateRecordInUpdate(this IGuardClause guardClause, List<EmployeeSalary> pList, Guid Id, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => x.Id != Id && ((x.StartDate >= startDate && x.StartDate <= endDate) || (x.EndDate >= startDate && x.EndDate <= endDate)));

            if (oModel != null)
            {
                var v = $"Date overlapped with {oModel.StartDate.Value.ToString("dd/MM/yyyy")} and {oModel.EndDate.Value.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }
    }
}
