using System;
using System.Collections.Generic;
using System.Text;

namespace KassaExpert.Util.Lib.Dto
{
    public sealed class CryptographicMaterialContainer
    {
        public string Base64AESKey { get; set; }

        public Dictionary<string, CertificateOrPublicKeyContainer> CertificateOrPublicKeyMap { get; set; }
    }

    public sealed class CertificateOrPublicKeyContainer
    {
        public string SignatureDeviceType { get; set; }

        public string SignatureCertificateOrPublicKey { get; set; }
    }
}