using FluentAssertions;
using KassaExpert.Util.Lib.Validation.Impl;
using NUnit.Framework;

namespace KassaExpert.Util.LibTest.ValidationTests
{
    [TestFixture]
    public class DefaultValidationTests
    {
        [TestCase("YES")]
        [TestCase("123456")]
        public void TestEncoding_Should_Be_True(string input)
        {
            var instance = new DefaultValidation();

            instance.IsValidUtf8String(input).Should().BeTrue();
        }

        [TestCase("𤽜")]
        [TestCase("𝄞")]
        [TestCase("ä")]
        public void TestEncoding_Should_Be_False(string input)
        {
            var instance = new DefaultValidation();

            instance.IsValidUtf8String(input).Should().BeFalse();
        }

        [TestCase("nonono")]
        [TestCase("1234567890abcdefg")]
        public void TestInvalidSerial(string invalidHex)
        {
            var instance = new DefaultValidation();
            instance.IsValidHexSerial(invalidHex).Should().BeFalse();
        }

        [TestCase("1234567890abcdef")]
        public void TestValidSerial(string validHex)
        {
            var instance = new DefaultValidation();
            instance.IsValidHexSerial(validHex).Should().BeTrue();
        }

        [TestCase("ATU73952234")]
        [TestCase("ATU73519007")]
        [TestCase("ATU67104705")]
        public void TestValidUid(string validUid)
        {
            var instance = new DefaultValidation();
            instance.IsValidUid(validUid).Should().BeTrue();
        }

        [TestCase("nonono")]
        [TestCase("1234567890abcdefg")]
        [TestCase("Atu12345678")]
        [TestCase("ATU 12 345 678")]
        [TestCase("ATU12345678 ")]
        [TestCase("ATU12345678")]
        public void TestInvalidUid(string invalidUid)
        {
            var instance = new DefaultValidation();
            instance.IsValidUid(invalidUid).Should().BeFalse();
        }
    }
}