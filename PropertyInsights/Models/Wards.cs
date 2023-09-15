using System.Text.Json.Serialization;

namespace PropertyInsights.Models
{
    public class Wards
    {
        [JsonPropertyName("ward")]
        public int Ward { get; set; }
    }
}
