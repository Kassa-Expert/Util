using FluentAssertions;
using KassaExpert.Util.Lib.Encoding;
using KassaExpert.Util.Lib.Encoding.Impl;
using NUnit.Framework;
using System;

namespace KassaExpert.Util.LibTest.EncodingTests
{
    [TestFixture]
    public class TestBase64UrlEncoding
    {
        private readonly IEncoding<string, string> _base64UrlEncoding = new Base64UrlEncoding();

        [Test]
        public void TestCanEncodeDecode()
        {
            var rand = new Random();

            for (long i = 1; i < 99999; i++)
            {
                byte[] testData = new byte[i];

                rand.NextBytes(testData);

                var base64TestData = Convert.ToBase64String(testData);

                var encrypted = _base64UrlEncoding.Encode(base64TestData);

                var decoded = _base64UrlEncoding.Decode(encrypted);

                decoded.Should().Be(base64TestData);
            }
        }

        [Test]
        public void TestExampleEncryption_1()
        {
            var input = "{\"alg\":\"ES256\"}";

            var base64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(input));

            var expected = _base64UrlEncoding.Encode(base64);

            expected.Should().Be("eyJhbGciOiJFUzI1NiJ9");
        }

        [Test]
        public void TestExampleEncryption_2()
        {
            var input = "_R1-AT100_CASHBOX-DEMO-1_CASHBOX-DEMO-1-Receipt-ID-1_2016-03-11T03:57:08_0,00_0,00_0,00_0,00_0,00_4r1iIdZG_82424e8077297aa3_cg8hNU5ihto=";

            var base64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(input/*.Replace("=", "")*/));

            var expected = _base64UrlEncoding.Encode(base64);

            expected.Should().Be("X1IxLUFUMTAwX0NBU0hCT1gtREVNTy0xX0NBU0hCT1gtREVNTy0xLVJlY2VpcHQtSUQtMV8yMDE2LTAzLTExVDAzOjU3OjA4XzAsMDBfMCwwMF8wLDAwXzAsMDBfMCwwMF80cjFpSWRaR184MjQyNGU4MDc3Mjk3YWEzX2NnOGhOVTVpaHRvPQ");
        }
    }
}