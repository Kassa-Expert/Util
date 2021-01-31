using KassaExpert.Util.Lib.Validation.Impl;

namespace KassaExpert.Util.Lib.Validation
{
    public interface IValidation
    {
        public static IValidation GetInstance() => DefaultValidation.GetInstance();

        bool IsValidUtf8String(string input);

        bool IsValidHexSerial(string hexSerial);

        bool IsValidUid(string uid);
    }
}