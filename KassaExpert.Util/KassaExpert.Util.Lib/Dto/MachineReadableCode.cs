using KassaExpert.Util.Lib.Date;
using KassaExpert.Util.Lib.Date.Impl;
using KassaExpert.Util.Lib.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace KassaExpert.Util.Lib.Dto
{
    public sealed class MachineReadableCode
    {
        public MachineReadableCode(string formatedCode)
        {
            var data = formatedCode.Split('_');

            if (data.Length != 13 && data.Length != 14)
            {
                throw new ArgumentOutOfRangeException(nameof(formatedCode), "Code must have correct format");
            }

            TrustProvider = TrustProvider.GetByAbbreviation(data[1].Substring(3));
            CashboxId = data[2];
            ReceiptNumber = data[3];
            AustiraDate = DateTime.Parse(data[4]);
            AmountTax20 = ReadAmount(data[5]);
            AmountTax10 = ReadAmount(data[6]);
            AmountTax13 = ReadAmount(data[7]);
            AmountTax0 = ReadAmount(data[8]);
            AmountTax19 = ReadAmount(data[9]);
            EncryptedRevenueCounter = data[10];
            CertificateSerialNumber = data[11];
            SignaturePreviousReceipt = data[12];

            if (data.Length == 14)
            {
                Signature = data[13];
            }

            decimal ReadAmount(string value)
            {
                return decimal.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
            }
        }

        public MachineReadableCode(TrustProvider trustProvider, string cashboxId,
            string receiptNumber, DateTime austiraDate,
            decimal amountTax20, decimal amountTax10, decimal amountTax13, decimal amountTax0, decimal amountTax19,
            string encryptedRevenueCounter, string certificateSerialNumber, string signaturePreviousReceipt, string? signature = null)
        {
            TrustProvider = trustProvider;
            CashboxId = cashboxId;
            ReceiptNumber = receiptNumber;
            AustiraDate = austiraDate;
            AmountTax20 = amountTax20;
            AmountTax10 = amountTax10;
            AmountTax13 = amountTax13;
            AmountTax0 = amountTax0;
            AmountTax19 = amountTax19;
            EncryptedRevenueCounter = encryptedRevenueCounter;
            CertificateSerialNumber = certificateSerialNumber;
            SignaturePreviousReceipt = signaturePreviousReceipt;
            Signature = signature;
        }

        public TrustProvider TrustProvider { get; }

        /// <summary>
        /// Kassen-ID
        /// MUST BE UTF-8 <see cref="Validation.IValidation.IsUtf8String(string)"/>
        /// </summary>
        public string CashboxId { get; }

        /// <summary>
        /// Belegnummer
        /// </summary>
        public string ReceiptNumber { get; }

        /// <summary>
        /// Beleg-Datum-Uhrzeit
        /// </summary>
        public DateTime AustiraDate { get; }

        /// <summary>
        /// Betrag-Satz-Normal
        /// </summary>
        public decimal AmountTax20 { get; }

        /// <summary>
        /// Betrag-Satz-Ermaessigt-1
        /// </summary>
        public decimal AmountTax10 { get; }

        /// <summary>
        /// Betrag-Satz-Ermaessigt-2
        /// </summary>
        public decimal AmountTax13 { get; }

        /// <summary>
        /// Betrag-Satz-Null
        /// </summary>
        public decimal AmountTax0 { get; }

        /// <summary>
        /// Betrag-Satz-Besonders
        /// </summary>
        public decimal AmountTax19 { get; }

        /// <summary>
        /// Stand-Umsatz-Zaehler-AES256-ICM
        /// </summary>
        public string EncryptedRevenueCounter { get; }

        /// <summary>
        /// Zertifikat-Seriennummer [HEX ohne prefix 0x] <see cref="Validation.IValidation.IsValidHexSerial(string)"/>
        /// </summary>
        public string CertificateSerialNumber { get; }

        /// <summary>
        /// Sig-Voriger-Beleg
        /// </summary>
        public string SignaturePreviousReceipt { get; }

        public string? Signature { get; internal set; }

        public string GetCode()
        {
            IDate dateConverter = new DefaultDate();

            var sb = new StringBuilder();

            sb.Append("_R1-");
            sb.Append(TrustProvider.Abbreviation);
            sb.Append('_');
            sb.Append(CashboxId);
            sb.Append('_');
            sb.Append(ReceiptNumber);
            sb.Append('_');
            sb.Append(dateConverter.FormatDate(AustiraDate));
            sb.Append('_');
            sb.Append(FormatAmount(AmountTax20));
            sb.Append('_');
            sb.Append(FormatAmount(AmountTax10));
            sb.Append('_');
            sb.Append(FormatAmount(AmountTax13));
            sb.Append('_');
            sb.Append(FormatAmount(AmountTax0));
            sb.Append('_');
            sb.Append(FormatAmount(AmountTax19));
            sb.Append('_');
            sb.Append(EncryptedRevenueCounter);
            sb.Append('_');
            sb.Append(CertificateSerialNumber);
            sb.Append('_');
            sb.Append(SignaturePreviousReceipt);

            if (Signature is not null)
            {
                sb.Append('_');
                sb.Append(Signature!);
            }

            return sb.ToString();
        }

        internal static string FormatAmount(decimal amount)
        {
            return Math.Round(amount, 2).ToString("0.00", CultureInfo.InvariantCulture).Replace('.', ',');
        }
    }
}