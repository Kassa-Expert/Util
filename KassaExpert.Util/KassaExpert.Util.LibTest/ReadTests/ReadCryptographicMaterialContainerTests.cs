using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KassaExpert.Util.LibTest.ReadTests
{
    [TestFixture]
    public class ReadCryptographicMaterialContainerTests
    {
        [Test]
        public void ReadAllCryptographicMaterialContainerInDirectorySubdirectory()
        {
            var baseDirectory = Helper.TryGetSolutionDirectoryInfo().Parent;

            var directoryName = Path.Combine(baseDirectory.FullName, @"TEST_CASES_V1.2\open system");

            Directory.Exists(directoryName).Should().BeTrue();
        }
    }
}