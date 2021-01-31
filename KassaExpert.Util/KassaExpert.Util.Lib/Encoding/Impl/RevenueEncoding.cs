using System;

namespace KassaExpert.Util.Lib.Encoding.Impl
{
    internal sealed class RevenueEncoding : IEncoding<long, byte[]>
    {
        private static readonly Lazy<RevenueEncoding> _instance = new Lazy<RevenueEncoding>(() => new RevenueEncoding(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        internal static IEncoding<long, byte[]> GetInstance() => _instance.Value;

        public long Decode(byte[] source)
        {
            var copyOfElement = new byte[source.Length];

            Buffer.BlockCopy(source, 0, copyOfElement, 0, source.Length);

            Array.Reverse(copyOfElement, 0, copyOfElement.Length);

            return BitConverter.ToInt64(copyOfElement);
        }

        public byte[] Encode(long source)
        {
            byte[] umsatzBytes = BitConverter.GetBytes(source);
            Array.Resize(ref umsatzBytes, 16);

            // reverse to get big-endian array
            Array.Reverse(umsatzBytes, 0, umsatzBytes.Length);
            return umsatzBytes;
        }
    }
}