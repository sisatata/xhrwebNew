using Ardalis.GuardClauses;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using LeaveManagement.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaveManagement.Core.ExtensionMethods
{
    public static class LeaveContextGuard
    {
        public static void DuplicateRecords(this IGuardClause guardClause, List<LeaveApplication> pList, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => (x.StartDate >= startDate && x.StartDate <= endDate) || (x.EndDate >= startDate && x.EndDate <= endDate));

            if (oModel != null)
            {
                var v = $"Leave date duplicated with {oModel.StartDate.ToString("dd/MM/yyyy")} and {oModel.EndDate.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void DuplicateRecordInUpdate(this IGuardClause guardClause, List<LeaveApplication> pList, Guid Id, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            var oModel = pList.FirstOrDefault(x => x.Id != Id && ((x.StartDate >= startDate && x.StartDate <= endDate) || (x.EndDate >= startDate && x.EndDate <= endDate)));

            if (oModel != null)
            {
                var v = $"Leave date duplicated with {oModel.StartDate.ToString("dd/MM/yyyy")} and {oModel.EndDate.ToString("dd/MM/yyyy")}";
                throw new ArgumentException(v, parameterName);
            }
        }

        public static void InvalidApplicationStatus(this IGuardClause guardClause, string input, string checkWith, 
            string parameterName, string message = null)
        {
            if(input == checkWith)
            {
                throw new ArgumentException(message ?? "Not a valid status", parameterName);
            }
        }

        public static void InvalidLeaveDate(this IGuardClause guardClause, DateTime startDate, DateTime endDate,
            string parameterName, string message = null)
        {
            if (endDate < startDate)
            {
                throw new ArgumentException(message ?? "Not a valid start & end date", parameterName);
            }
        }
        public static void InsufficientBalance(this IGuardClause guardClause, double balance, double requestDay,
            string parameterName, string message = null)
        {
            if (balance < requestDay)
            {
                throw new ArgumentException(message ?? "Insufficient leave balance", parameterName);
            }
        }
        public static void AdvanceLeaveApplication(this IGuardClause guardClause, bool isAllowAdvanceApplication, DateTime startDate, string parameterName, string message = null)
        {
            if (!isAllowAdvanceApplication && startDate > DateTime.Now)
            {
                throw new ArgumentException(message ?? "Advance application is not allowed", parameterName);
            }
        }
        public static void PostLeaveApplication(this IGuardClause guardClause, bool isAllowPostApplication, DateTime startDate, string parameterName, string message = null)
        {
            if (isAllowPostApplication && startDate < DateTime.Now)
            {
                throw new ArgumentException(message ?? "Post application is not allowed", parameterName);
            }
        }
    }
}
