using KassaExpert.Util.Lib.Date.Impl;
using System;

namespace KassaExpert.Util.Lib.Date
{
    public interface IDate
    {
        public static IDate GetInstance() => DefaultDate.GetInstance();

        DateTime GetMezNow();

        string FormatDate(DateTime mezDate);
    }
}