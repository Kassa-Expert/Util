using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.Lib.Validation
{
    public interface IValidation
    {
        bool IsUtf8String(string input);

        bool IsValidHexSerial(string hexSerial);
    }
}