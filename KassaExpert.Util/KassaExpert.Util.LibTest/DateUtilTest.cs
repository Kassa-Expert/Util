using FluentAssertions;
using KassaExpert.Util.Lib;
using KassaExpert.Util.Lib.Date.Impl;
using NUnit.Framework;
using System;

namespace KassaExpert.Util.LibTest
{
    [TestFixture]
    public class DateUtilTest
    {
        [Test]
        public void TestDateConversion()
        {
            var dateUtil = new DefaultDate();

            var now = DateTime.Now;

            var nowConverted = now.ToUniversalTime();

            now.Should().Be(dateUtil.ConvertToAustira(nowConverted));
        }

        [Test]
        public void TestDateFormat()
        {
            var dateUtil = new DefaultDate();

            var now = DateTime.Now;

            dateUtil.FormatDate(now)[10].Should().Be('T');
            dateUtil.FormatDate(now).Should().Be(now.ToString("yyyy-MM-ddTHH:mm:ss"));
        }
    }
}