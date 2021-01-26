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

            instance.IsUtf8String(input).Should().BeTrue();
        }

        [TestCase("𤽜")]
        [TestCase("𝄞")]
        [TestCase("ä")]
        public void TestEncoding_Should_Be_False(string input)
        {
            var instance = new DefaultValidation();

            instance.IsUtf8String(input).Should().BeFalse();
        }
    }
}