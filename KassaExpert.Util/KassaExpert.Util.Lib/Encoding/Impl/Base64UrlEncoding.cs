using System;

namespace KassaExpert.Util.Lib.Encoding.Impl
{
    internal sealed class Base64UrlEncoding : IEncoding<string, string>
    {
        private static readonly Lazy<Base64UrlEncoding> _instance = new Lazy<Base64UrlEncoding>(() => new Base64UrlEncoding(), System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);

        internal static IEncoding<string, string> GetInstance() => _instance.Value;

        private static readonly char[] padding = { '=' };

        /// <summary>
        /// Converts a base64URL string to base64 string
        /// </summary>
        public string Decode(string source)
        {
            string output = source.Replace('_', '/').Replace('-', '+');
            switch (output.Length % 4)
            {
                case 2: output += "=="; break;
                case 3: output += "="; break;
            }

            return output;
        }

        /// <summary>
        /// Converts a base64 string to base64URL string
        /// </summary>
        public string Encode(string source)
        {
            return source.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }
    }
}