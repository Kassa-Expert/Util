﻿using KassaExpert.Util.Lib.Encoding;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace KassaExpert.Util.Lib.Encryption.Impl
{
    internal sealed class DefaultEncryption : IEncryption
    {
        private static readonly Lazy<DefaultEncryption> _instance = new Lazy<DefaultEncryption>(() => new DefaultEncryption(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        internal static IEncryption GetInstance() => _instance.Value;

        private readonly IEncoding<long, byte[]> _revenueEncoding = IEncoding.GetRevenueEncoding();

        private const string _cipherSuite = "AES/CTR/NoPadding";

        public byte[] GenerateIV(string cashregisterIdentification, long receiptNumber)
        {
            byte[] inBytes = System.Text.Encoding.UTF8.GetBytes(cashregisterIdentification + receiptNumber.ToString());
            using (var sha256hash = SHA256.Create())
            {
                return sha256hash.ComputeHash(inBytes).Take(16).ToArray();
            }
        }

        public string EncryptRevenueCounter(long revenue, string cashregisterIdentification, long receiptNumber, byte[] aesKey)
        {
            var encryptedRevenueCounter = Encrypt(_revenueEncoding.Encode(revenue), GenerateIV(cashregisterIdentification, receiptNumber), aesKey);
            return Convert.ToBase64String(encryptedRevenueCounter);
        }

        private IBufferedCipher GenerateCipher(bool encryption, byte[] iv, byte[] aesKey)
        {
            var cipher = CipherUtilities.GetCipher(_cipherSuite);

            try
            {
                KeyParameter skey = new KeyParameter(aesKey);
                ParametersWithIV aesIVKeyParam = new ParametersWithIV(skey, iv);
                cipher.Init(encryption, aesIVKeyParam);
            }
            catch (Exception e)
            {
                Console.WriteLine("Encrypt exception (init): " + e.Message);
                throw;
            }

            return cipher;
        }

        public byte[] Encrypt(byte[] data, byte[] iv, byte[] aesKey)
        {
            IBufferedCipher cipher = GenerateCipher(true, iv, aesKey);

            byte[] outputBytes = new byte[cipher.GetOutputSize(data.Length)];

            int length = cipher.ProcessBytes(data, outputBytes, 0);
            cipher.DoFinal(outputBytes, length);

            return outputBytes;
        }

        public byte[] Decrypt(byte[] data, byte[] iv, byte[] aesKey)
        {
            IBufferedCipher cipher = GenerateCipher(false, iv, aesKey);

            byte[] comparisonBytes = new byte[cipher.GetOutputSize(data.Length)];

            int length = cipher.ProcessBytes(data, comparisonBytes, 0);
            cipher.DoFinal(comparisonBytes, length);

            return comparisonBytes;
        }

        public byte[] GenerateAesKey()
        {
            using (var aesKey = new AesManaged())
            {
                aesKey.KeySize = 256;
                aesKey.GenerateKey();

                return aesKey.Key;
            }
        }
    }
}