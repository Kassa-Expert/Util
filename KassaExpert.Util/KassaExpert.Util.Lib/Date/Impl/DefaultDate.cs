using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.Lib.Date.Impl
{
    public class DefaultDate : IDate
    {
        private const string _timeZoneString = "W. Europe Standard Time";

        private const string _dateFormatString = "yyyy-MM-ddTHH:mm:ss";

        private static Lazy<TimeZoneInfo> _timeZoneInfo => new Lazy<TimeZoneInfo>(() => TimeZoneInfo.FindSystemTimeZoneById(_timeZoneString), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        internal DateTime ConvertToAustira(DateTime input)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(input, _timeZoneInfo.Value);
        }

        public DateTime GetMezNow()
        {
            return ConvertToAustira(DateTime.UtcNow);
        }

        public string FormatDate(DateTime mezDate)
        {
            return mezDate.ToString(_dateFormatString);
        }
    }
}