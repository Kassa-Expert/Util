using KassaExpert.Util.Lib.Encoding.Impl;

namespace KassaExpert.Util.Lib.Encoding
{
    public interface IEncoding
    {
        /// <summary>
        /// Converts a BASE-64 string to a BASE-64-URL string and back
        /// </summary>
        public static IEncoding<string, string> GetBase64ToBase64UrlEncoding() => Base64UrlEncoding.GetInstance();

        /// <summary>
        /// Converts a long-number to a big-endian array ready for aes-encryption (and back)
        /// </summary>
        public static IEncoding<long, byte[]> GetRevenueEncoding() => RevenueEncoding.GetInstance();
    }

    public interface IEncoding<TPlain, TEncoded> : IEncoding
    {
        TEncoded Encode(TPlain source);

        TPlain Decode(TEncoded source);
    }
}