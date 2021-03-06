﻿using FluentAssertions;
using KassaExpert.Util.Lib.Dto;
using KassaExpert.Util.Lib.Encoding;
using KassaExpert.Util.Lib.Encoding.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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

            var base64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(input));

            var expected = _base64UrlEncoding.Encode(base64);

            expected.Should().Be("X1IxLUFUMTAwX0NBU0hCT1gtREVNTy0xX0NBU0hCT1gtREVNTy0xLVJlY2VpcHQtSUQtMV8yMDE2LTAzLTExVDAzOjU3OjA4XzAsMDBfMCwwMF8wLDAwXzAsMDBfMCwwMF80cjFpSWRaR184MjQyNGU4MDc3Mjk3YWEzX2NnOGhOVTVpaHRvPQ");
        }

        [Test]
        public async Task TestAllFromTestFolder()
        {
            var baseDirectory = Helper.TryGetSolutionDirectoryInfo().Parent;

            var directoryName = Path.Combine(baseDirectory.FullName, @"TEST_CASES_V1.2\open system");

            Directory.Exists(directoryName).Should().BeTrue();

            foreach (var subDirectory in Directory.GetDirectories(directoryName))
            {
                File.Exists(Path.Combine(subDirectory, "dep-export.json")).Should().BeTrue();
                File.Exists(Path.Combine(subDirectory, "qr-code-rep.json")).Should().BeTrue();

                var orderedJwsItems = await GetJwsItemsFromFile(Path.Combine(subDirectory, "dep-export.json"));
                var orderedMachineItems = await GetMachineItemsFromFile(Path.Combine(subDirectory, "qr-code-rep.json"));

                orderedJwsItems.Should().NotBeNullOrEmpty();
                orderedMachineItems.Should().NotBeNullOrEmpty();

                orderedJwsItems.Should().HaveSameCount(orderedMachineItems);

                //test if every base-64 code is correctly generated
                for (int i = 0; i < orderedJwsItems.Length; i++)
                {
                    orderedMachineItems[i].Signature = null;

                    var base64 = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(orderedMachineItems[i].GetCode()));

                    var expected = _base64UrlEncoding.Encode(base64);

                    expected.Should().Be(orderedJwsItems[i].Payload);
                }
            }
        }

        private static async Task<MachineReadableCode[]> GetMachineItemsFromFile(string file)
        {
            using (FileStream openStream = File.OpenRead(file))
            {
                var data = await JsonSerializer.DeserializeAsync<List<string>>(openStream);

                data.Should().NotBeNull().And.NotBeEmpty();

                var resultList = new MachineReadableCode[data.Count];

                for (int i = 0; i < data.Count; i++)
                {
                    resultList[i] = new MachineReadableCode(data[i]);
                }

                return resultList;
            }
        }

        private static async Task<JwsItem[]> GetJwsItemsFromFile(string file)
        {
            using (FileStream openStream = File.OpenRead(file))
            {
                var data = await JsonSerializer.DeserializeAsync<DepExportFormat>(openStream);

                data.Should().NotBeNull(file);

                data.ReceiptList.Should().NotBeNullOrEmpty(file);

                data.ReceiptList.First().Receipts.Should().NotBeNullOrEmpty(file);

                var resList = new JwsItem[data.ReceiptList.First().Receipts.Length];

                for (int i = 0; i < resList.Length; i++)
                {
                    resList[i] = new JwsItem(data.ReceiptList.First().Receipts[i]);
                }

                return resList;
            }
        }
    }
}