using OnlineStore.Infrastructure.Exceptions;
using System;
using System.Runtime.InteropServices;

namespace OnlineStore.Infrastructure.Extensions
{
    public static class DateExtension
    {
        /// <exception cref="OSPlatformException">
        /// Thrown when runtime system isn't Windows or Linux.
        /// </exception>
        public static DateTimeOffset ConvertUtcToPolishTime(this DateTimeOffset date)
        {
            DateTimeOffset? convertedDate = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                convertedDate = TimeZoneInfo
                    .ConvertTimeBySystemTimeZoneId(date, "Central European Standard Time");
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                convertedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(date, "Europe/Warsaw");

            if (convertedDate == null)
                throw new OSPlatformException();

            return (DateTimeOffset) convertedDate;
        }
    }
}
