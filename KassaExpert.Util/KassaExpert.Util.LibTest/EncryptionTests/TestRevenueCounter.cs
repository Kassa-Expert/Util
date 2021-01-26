using FluentAssertions;
using KassaExpert.Util.Lib.Encryption;
using KassaExpert.Util.Lib.Encryption.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.LibTest.EncryptionTests
{
    [TestFixture]
    public class TestRevenueCounter
    {
        private readonly IEncryption _encryption = new DefaultEncryption();

        [Test]
        public void TestRevenueCounterEncoding()
        {
            for (long i = 0; i < 999999999999; i++)
            {
                var encrypted = _encryption.EncodeRevenue(i);

                var decoded = _encryption.DecodeRevenue(encrypted);

                decoded.Should().Be(i);
            }
        }

        [Test]
        public void TestRevenueEncryption()
        {
            var aesKey = _encryption.GenerateAesKey();

            //_encryption.
        }
    }
}