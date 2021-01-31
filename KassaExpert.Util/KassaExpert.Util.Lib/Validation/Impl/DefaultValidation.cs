using System.Text.RegularExpressions;

namespace KassaExpert.Util.Lib.Validation.Impl
{
    internal class DefaultValidation : IValidation
    {
        /// <summary>
        /// für den Initialisierungs-Vektor wird Kassen-Identifikator+Belegnummer im UTF8-Format verwendet
        /// Es sollte kein Problem darstellen wenn zb. ein Ä verwendet wird, kann aber durchaus passieren da es nicht in der spezifikation steht
        /// </summary>
        public bool IsValidUtf8String(string input)
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

        /// <summary>
        /// BSP: ATU1234678
        /// </summary>
        public bool IsValidUid(string uid)
        {
            if (string.IsNullOrEmpty(uid))
            {
                return false;
            }

            if (!Regex.IsMatch(uid, @"^ATU[a-zA-Z0-9]{8}$"))
            {
                return false;
            }

            var lastDigit = int.Parse(uid[10].ToString());

            return lastDigit == CalcCheckSumAustria(uid.Substring(3));
        }

        /// <summary>
        /// https://www.bmf.gv.at/dam/jcr:9f9f8d5f-5496-4886-aa4f-81a4e39ba83e/BMF_UID_Konstruktionsregeln.pdf
        /// </summary>
        internal static int CalcCheckSumAustria(string numberPart)
        {
            var s3 = calcS(3);
            var s5 = calcS(5);
            var s7 = calcS(7);

            var r = s3 + s5 + s7;

            return (10 - ((r + c(2) + c(4) + c(6) + c(8) + 4) % 10)) % 10;

            int c(int position)
            {
                var index = position - 2;
                return int.Parse(numberPart[index].ToString());
            }

            int calcS(int position)
            {
                var ci = c(position);

                return ((ci / 5) + (ci * 2) % 10);
            }
        }
    }
}