using System;

namespace ASL.Hrms.SharedKernel.ExtensionMethods
{
    public static class DateTimeExtension
    {
        // https://stackoverflow.com/questions/1525990/calculating-the-difference-in-months-between-two-dates
        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        public static int GetMonthsBetweenDate(this DateTime from, DateTime to)
        {
            if (from > to) return GetMonthsBetweenDate(to, from);

            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
            {
                return monthDiff - 1;
            }
            else
            {
                return monthDiff;
            }
        }
    }
}
