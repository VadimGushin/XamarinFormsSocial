using System;
using LocalStrings = XamarinSocial.Resources.Strings.Strings;

namespace XamarinSocial.Extensions
{
    public static class ChatItemDateTimeExtension
    {
        public static string GetTimeString(this DateTime dateTime, bool isStatusText = false)
        {
            string result = string.Empty;
            var currentTime = DateTime.Now;
            var elapsedTicks = currentTime.Ticks - dateTime.Ticks;

            if (elapsedTicks <= 0)
            {
                return "---";
            }

            var elapsedSpan = new TimeSpan(elapsedTicks);
            int fullYears =(int)Math.Round((double)(elapsedSpan.Days / 365));
            var fullDays = elapsedSpan.Days;
            var fullHours = elapsedSpan.Hours;
            var fullMinutes = elapsedSpan.Minutes;
            var statusText = isStatusText ? "ago" : string.Empty;

            if (fullYears >= 1)
            {
                string yearsEnd = fullYears > 1 ? "s" : string.Empty;
                result = $"{fullYears} year{yearsEnd} ago";
            }
            if (fullYears == 0 && fullDays >= DateTime.DaysInMonth(currentTime.Year, currentTime.Month))
            {
                var fullMonths = (int)Math.Round((double)(fullDays / 30));
                string monthEnd = fullMonths > 1 ? "s" : string.Empty;
                result = $"{fullMonths} month{monthEnd} ago";
                return result;
            }
            if (fullDays < DateTime.DaysInMonth(currentTime.Year, currentTime.Month) && fullDays > 1)
            {
                result = $"{fullDays} days ago";
            }
            if (fullDays == 1)
            {
                result = LocalStrings.Yesterday.ToLower();
            }
            if (fullDays == 0 && fullHours >= 1)
            {
                result = $"{fullHours}h {fullMinutes}min {statusText}";
            }
            if (fullDays == 0 && fullHours == 0)
            {
                result = $"{fullMinutes}m {statusText}";
            }

            return result;
        }
    }
}
