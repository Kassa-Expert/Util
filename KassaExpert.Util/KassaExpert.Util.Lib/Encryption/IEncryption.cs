using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.Lib.Encryption
{
    public interface IEncryption
    {
        string EncryptRevenueCounter(long revenue, string cashregisterIdentification, long receiptNumber, byte[] aesKey);

        /// <summary>
        /// Used in <see cref="EncryptRevenueCounter(long, string, long, byte[])"/>
        /// </summary>
        byte[] EncodeRevenue(long revenue);

        long DecodeRevenue(byte[] encodedRevenue);

        /// <summary>
        /// Used in <see cref="EncryptRevenueCounter(long, string, long, byte[])"/>
        /// </summary>
        byte[] GenerateIV(string cashregisterIdentification, long receiptNumber);

        byte[] Encrypt(byte[] data, byte[] iv, byte[] aesKey);

        byte[] Decrypt(byte[] data, byte[] iv, byte[] aesKey);

        /// <summary>
        /// To generate a new AES key for a new Cash-Register
        /// </summary>
        /// <returns></returns>
        byte[] GenerateAesKey();
    }
}