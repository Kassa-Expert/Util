using FluentAssertions;
using KassaExpert.Util.Lib.Dto;
using KassaExpert.Util.Lib.Enum;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.LibTest.DtoTests
{
    [TestFixture]
    public class TestMachineReadableCode
    {
        [Test]
        public void TestGetCode()
        {
            var tmp = new MachineReadableCode(TrustProvider.A_Trust, "DEMO-CASH-BOX524", "366585AB",
                new DateTime(2015, 12, 17, 11, 23, 43), 10.50m, 0.0m, 0.0m, 0.78m, 0.0m, "Q+dTEnSc", "245abcde", "OJ16FcqeA7s");

            var resultCode = tmp.GetCode();

            resultCode.Should().Be("_R1-AT1_DEMO-CASH-BOX524_366585AB_2015-12-17T11:23:43_10,50_0,00_0,00_0,78_0,00_Q+dTEnSc_245abcde_OJ16FcqeA7s");
        }
    }
}