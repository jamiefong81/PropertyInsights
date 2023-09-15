using System.Text.Json.Serialization;

namespace PropertyInsights.Models
{
    public class Location
    {
        [JsonPropertyName("latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("longtitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("human_address")]
        public HumanAddress? HumanAddress { get; set; }
    }
}
