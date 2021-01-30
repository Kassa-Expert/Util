using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KassaExpert.Util.Lib.Dto
{
    public sealed class DepExportFormat
    {
        [JsonPropertyName("Belege-Gruppe")]
        public DepReceiptDump[] ReceiptList { get; set; }
    }
}