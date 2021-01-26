using FluentAssertions;
using KassaExpert.Util.Lib.Encoding;
using KassaExpert.Util.Lib.Encoding.Impl;
using NUnit.Framework;

namespace KassaExpert.Util.LibTest.EncodingTests
{
    [TestFixture]
    public class TestRevenueEncoding
    {
        private readonly IEncoding<long, byte[]> _revenueEncoding = new RevenueEncoding();

        [Test]
        public void TestCanEncodeDecode()
        {
            for (long i = 0; i < 999999999; i++)
            {
                var encrypted = _revenueEncoding.Encode(i);

                var decoded = _revenueEncoding.Decode(encrypted);

                decoded.Should().Be(i);
            }
        }
    }
}