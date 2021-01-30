using FluentAssertions;
using KassaExpert.Util.Lib.Dto;
using KassaExpert.Util.Lib.Validation;
using KassaExpert.Util.Lib.Validation.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KassaExpert.Util.LibTest.ReadTests
{
    [TestFixture]
    public class ReadJwsItemsTests
    {
        [Test]
        public async Task ReadAllJwsItemsTests()
        {
            var baseDirectory = Helper.TryGetSolutionDirectoryInfo().Parent;

            var directoryName = Path.Combine(baseDirectory.FullName, @"TEST_CASES_V1.2\open system");

            Directory.Exists(directoryName).Should().BeTrue();

            var files = Helper.GetFilesInFolderSubFolder(directoryName, "dep-export.json");

            files.Should().NotBeEmpty();

            foreach (var file in files)
            {
                Console.WriteLine(file);

                using (FileStream openStream = File.OpenRead(file))
                {
                    var data = await JsonSerializer.DeserializeAsync<DepExportFormat>(openStream);

                    data.Should().NotBeNull(file);

                    data.ReceiptList.Should().NotBeNullOrEmpty(file);

                    data.ReceiptList.First().Receipts.Should().NotBeNullOrEmpty(file);

                    foreach (var item in data.ReceiptList.First().Receipts)
                    {
                        var jwsItem = new JwsItem(item);

                        jwsItem.Should().NotBeNull();

                        jwsItem.Payload.Should().NotBeNullOrEmpty();
                        jwsItem.Signature.Should().NotBeNullOrEmpty();
                        jwsItem.Header.Should().NotBeNullOrEmpty();
                    }
                }
            }
        }
    }
}