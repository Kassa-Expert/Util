using System.Text.RegularExpressions;

namespace KassaExpert.Util.Lib.Validation.Impl
{
    internal class DefaultValidation : IValidation
    {
        /// <summary>
        /// für den Initialisierungs-Vektor wird Kassen-Identifikator+Belegnummer im UTF8-Format verwendet
        /// Es sollte kein Problem darstellen wenn zb. ein Ä verwendet wird, kann aber durchaus passieren da es nicht in der spezifikation steht
        /// </summary>
        public bool IsUtf8String(string input)
        {
            //bei UTF8 hat jedes Zeichen exakt 1 byte -> die größe in Byte sollte also der länge entsprechen
            return System.Text.Encoding.UTF8.GetByteCount(input) == input.Length;
        }

        public bool IsValidHexSerial(string hexSerial)
        {
            var trimmed = hexSerial.Trim();

            if (string.IsNullOrEmpty(trimmed))
            {
                return false;
            }

            //der Präfix 0x wird nicht angegeben
            if (trimmed.StartsWith("0x"))
            {
                return false;
            }

            return Regex.IsMatch(trimmed, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}