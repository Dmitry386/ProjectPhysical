using System;
using System.Globalization;

namespace DVUnityUtilities
{
    public static class TimeUtils
    {
        public static string Hour24To12Format(int hour24, string am = "am", string pm = "pm")
        {
            DateTime dateTime = new DateTime(1, 1, 1, hour24, 0, 0);
            CultureInfo culture = CultureInfo.InvariantCulture;

            string hour12 = dateTime.ToString("hh tt", culture);

            string res = (hour12);
            res = res.Replace("AM", am);
            res = res.Replace("PM", pm);

            return res;
        }
    }
}
