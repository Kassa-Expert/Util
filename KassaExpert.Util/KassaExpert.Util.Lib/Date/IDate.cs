using System;

namespace KassaExpert.Util.Lib.Date
{
    public interface IDate
    {
        DateTime GetMezNow();

        string FormatDate(DateTime mezDate);
    }
}