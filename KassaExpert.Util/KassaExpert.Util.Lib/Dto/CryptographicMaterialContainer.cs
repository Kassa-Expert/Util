using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KassaExpert.Util.Lib.Dto
{
    public sealed class CryptographicMaterialContainer
    {
        [JsonPropertyName("base64AESKey")]
        public string Base64AESKey { get; set; }

        [JsonPropertyName("certificateOrPublicKeyMap")]
        public Dictionary<string, CertificateOrPublicKeyContainer> CertificateOrPublicKeyMap { get; set; }
    }

    public sealed class CertificateOrPublicKeyContainer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("signatureDeviceType")]
        public string SignatureDeviceType { get; set; }

        [JsonPropertyName("signatureCertificateOrPublicKey")]
        public string SignatureCertificateOrPublicKey { get; set; }
    }
}