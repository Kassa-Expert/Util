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
    public class ReadMachineReadableCodeListTests
    {
        [Test]
        public async Task ReadAllMachineReadableCodeListTests()
        {
            var baseDirectory = Helper.TryGetSolutionDirectoryInfo().Parent;

            var directoryName = Path.Combine(baseDirectory.FullName, @"TEST_CASES_V1.2\open system");

            Directory.Exists(directoryName).Should().BeTrue();

            var files = Helper.GetFilesInFolderSubFolder(directoryName, "qr-code-rep.json");

            files.Should().NotBeEmpty();

            foreach (var file in files)
            {
                Console.WriteLine(file);

                using (FileStream openStream = File.OpenRead(file))
                {
                    var data = await JsonSerializer.DeserializeAsync<List<string>>(openStream);

                    data.Should().NotBeNull().And.NotBeEmpty();

                    foreach (var entry in data)
                    {
                        var machineReadableCode = new MachineReadableCode(entry);
                        machineReadableCode.Should().NotBeNull();
                    }
                }
            }
        }
    }
}