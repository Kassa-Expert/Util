using System.Text.Json.Serialization;

namespace KassaExpert.Util.Lib.Dto
{
    public sealed class DepExportFormat
    {
        [JsonPropertyName("Belege-Gruppe")]
        public DepReceiptDump[] ReceiptList { get; set; }
    }
}