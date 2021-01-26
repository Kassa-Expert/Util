using FluentAssertions;
using KassaExpert.Util.Lib.Dto;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.LibTest.DtoTests
{
    [TestFixture]
    public class TestJwsItem
    {
        [Test]
        public void TestJwsCombinedConstructor_1()
        {
            Action act = () => new JwsItem("hi");
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TestJwsCombinedConstructor_2()
        {
            Action act = () => new JwsItem("hi.hi");
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TestJwsCombinedConstructor_3()
        {
            Action act = () => new JwsItem(string.Empty);
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TestJwsCombinedConstructor_4()
        {
            var item = new JwsItem("hi.hi.hi");

            item.Header.Should().Be("hi");
            item.Payload.Should().Be("hi");
            item.Signature.Should().Be("hi");
        }
    }
}