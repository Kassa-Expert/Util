using Enum.Ext;

namespace KassaExpert.Util.Lib.Enum
{
    public sealed class TrustProvider : TypeSafeEnum<TrustProvider, int>
    {
        public static readonly TrustProvider A_Trust = new TrustProvider(1, "AT1");
        public static readonly TrustProvider GlobalTrust = new TrustProvider(2, "AT2");
        public static readonly TrustProvider TEST = new TrustProvider(9, "AT9");

        private TrustProvider(int id, string abbreviation) : base(id)
        {
            Abbreviation = abbreviation;
        }

        public string Abbreviation { get; }
    }
}