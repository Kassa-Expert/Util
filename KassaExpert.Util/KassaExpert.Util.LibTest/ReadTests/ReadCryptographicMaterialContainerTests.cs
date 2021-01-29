using FluentAssertions;
using KassaExpert.Util.Lib.Dto;
using KassaExpert.Util.Lib.Validation;
using KassaExpert.Util.Lib.Validation.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KassaExpert.Util.LibTest.ReadTests
{
    [TestFixture]
    public class ReadCryptographicMaterialContainerTests
    {
        [Test]
        public async Task ReadAllCryptographicMaterialContainerInDirectorySubdirectory()
        {
            var baseDirectory = Helper.TryGetSolutionDirectoryInfo().Parent;

            var directoryName = Path.Combine(baseDirectory.FullName, @"TEST_CASES_V1.2\open system");

            Directory.Exists(directoryName).Should().BeTrue();

            var files = Helper.GetFilesInFolderSubFolder(directoryName, "cryptographicMaterialContainer.json");

            files.Should().NotBeEmpty();

            foreach (var file in files)
            {
                Console.WriteLine(file);

                using (FileStream openStream = File.OpenRead(file))
                {
                    var data = await JsonSerializer.DeserializeAsync<CryptographicMaterialContainer>(openStream);

                    data.Should().NotBeNull();

                    data.Base64AESKey.Should().NotBeNullOrEmpty();
                    Convert.FromBase64String(data.Base64AESKey).Should().NotBeNullOrEmpty();

                    data.CertificateOrPublicKeyMap.Should().NotBeNull().And.NotBeEmpty();
                }
            }
        }
    }
}