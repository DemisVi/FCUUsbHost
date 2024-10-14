using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json.Serialization;

namespace FCUUsbService.Models
{
    public struct DhcpClient
    {
        public readonly string MACAddress => MAC.ToString();
        [JsonIgnore]
        public PhysicalAddress MAC { get; init; }
        public readonly string IPAddress => IP.ToString();
        [JsonIgnore]
        public IPAddress IP { get; init; }
        public string HostName { get; init; }
        public DateTimeOffset Begin { get; init; }
        public DateTimeOffset End { get; init; }
    }
}
