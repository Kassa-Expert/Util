using System;

namespace KassaExpert.Util.Lib.Dto
{
    public sealed class JwsItem
    {
        public JwsItem(string combinedItem)
        {
            if (string.IsNullOrEmpty(combinedItem))
            {
                throw new ArgumentException(nameof(combinedItem), "jwsItem must not be empty");
            }

            var parts = combinedItem.Trim().Split('.');

            if (parts.Length != 3)
            {
                throw new ArgumentException(nameof(combinedItem), "jwsItem must have header, payload and signature");
            }

            Header = parts[0];
            Payload = parts[1];
            Signature = parts[2];
        }

        public string Header { get; }

        public string Payload { get; }

        public string Signature { get; }
    }
}