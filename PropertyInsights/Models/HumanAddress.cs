using System.Text.Json.Serialization;

namespace PropertyInsights.Models
{
    public class HumanAddress
    {
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("zip")]
        public int Zip { get; set; }
    }
}
