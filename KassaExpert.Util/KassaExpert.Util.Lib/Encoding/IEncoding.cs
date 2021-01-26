namespace KassaExpert.Util.Lib.Encoding
{
    public interface IEncoding<TPlain, TEncoded>
    {
        TEncoded Encode(TPlain source);

        TPlain Decode(TEncoded source);
    }
}