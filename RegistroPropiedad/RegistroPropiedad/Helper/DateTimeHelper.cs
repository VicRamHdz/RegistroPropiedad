using System;
using System.Globalization;

namespace RegistroPropiedad.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ToDateTime(this long value)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime date = start.AddMilliseconds(value).ToLocalTime();
            return date;
        }

        public static string ToCustomDateTime(this DateTime value)
        {
            return value.ToString("dd-MM-yyyy", new CultureInfo("es-MX"));
        }
    }
}
