using System.Text.Json.Serialization;

namespace KassaExpert.Util.Lib.Dto
{
    public class DepReceiptDump
    {
        [JsonPropertyName("Zertifizierungsstellen")]
        public string[] CertificateChain { get; set; }

        [JsonPropertyName("Signaturzertifikat")]
        public string Certificate { get; set; }

        [JsonPropertyName("Belege-kompakt")]
        public string[] Receipts { get; set; }
    }
}