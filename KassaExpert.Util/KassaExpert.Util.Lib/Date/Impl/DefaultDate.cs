using System;

namespace KassaExpert.Util.Lib.Date.Impl
{
    internal sealed class DefaultDate : IDate
    {
        private static Lazy<DefaultDate> _instance => new Lazy<DefaultDate>(() => new DefaultDate(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        internal static IDate GetInstance() => _instance.Value;

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