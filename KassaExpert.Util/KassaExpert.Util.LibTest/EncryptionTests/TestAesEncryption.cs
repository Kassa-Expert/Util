using FluentAssertions;
using KassaExpert.Util.Lib.Encryption;
using KassaExpert.Util.Lib.Encryption.Impl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.LibTest.EncryptionTests
{
    [TestFixture]
    public class TestAesEncryption
    {
        private readonly IEncryption _encryption = new DefaultEncryption();

        [Test]
        public void TestEncryptionDecryption()
        {
            var aesKey = _encryption.GenerateAesKey();

            var kassenId = Guid.NewGuid().ToString();

            var rand = new Random();

            var belegNummer = rand.Next(0, int.MaxValue);

            var umsatz = rand.Next(0, int.MaxValue);

            byte[] input = new byte[2312];
            rand.NextBytes(input);

            var iv = _encryption.GenerateIV(kassenId, belegNummer);

            var encrypted = _encryption.Encrypt(input, iv, aesKey);

            iv = _encryption.GenerateIV(kassenId, belegNummer);

            var decrypted = _encryption.Decrypt(encrypted, iv, aesKey);

            decrypted.Length.Should().Be(input.Length);

            for (int i = 0; i < input.Length; i++)
            {
                decrypted[i].Should().Be(input[i]);
            }
        }
    }
}