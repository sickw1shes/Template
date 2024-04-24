using System.Data.Common;

namespace Template.Infrastructure.Common.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow.AddHours(3);
        }
    }
}
