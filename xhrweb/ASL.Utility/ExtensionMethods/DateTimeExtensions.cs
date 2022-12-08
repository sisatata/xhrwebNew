using System;

namespace ASL.Utility.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static long ConvertToUnixEpoch(this DateTime dateTime)
        {
            var epochTime = new DateTime(1970, 1, 1).ToLocalTime();
            var unixEpoch = (long)(dateTime - epochTime).TotalMilliseconds;
            return unixEpoch;
        }

        public static long ConvertToUnixEpochInSecond(this DateTime dateTime)
        {
            var epochTime = new DateTime(1970, 1, 1).ToLocalTime();
            var unixEpoch = (long)(dateTime - epochTime).TotalSeconds;
            return unixEpoch;
        }
    }
}
