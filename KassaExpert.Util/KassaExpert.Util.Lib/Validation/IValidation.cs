using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.Lib.Validation
{
    public interface IValidation
    {
        bool IsValidUtf8String(string input);

        bool IsValidHexSerial(string hexSerial);

        bool IsValidUid(string uid);
    }
}