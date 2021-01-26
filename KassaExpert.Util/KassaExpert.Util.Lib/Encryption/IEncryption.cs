using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.Lib.Encryption
{
    public interface IEncryption
    {
        string EncryptRevenueCounter(long revenue, string cashregisterIdentification, long receiptNumber, byte[] aesKey);

        byte[] EncodeRevenue(long revenue);

        public byte[] GenerateIV(string cashregisterIdentification, long receiptNumber);

        byte[]? Encrypt(byte[] data, byte[] IV, byte[] aesKey);

        byte[]? Decrypt(byte[] data, byte[] iv, byte[] aesKey);

        byte[] GenerateAesKey();
    }
}