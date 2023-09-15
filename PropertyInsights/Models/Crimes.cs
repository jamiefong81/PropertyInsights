using System.Text.Json.Serialization;

namespace PropertyInsights.Models
{
    public class Crimes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("case_number")]
        public string CaseNumber { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("block")]
        public string Block { get; set; }

        [JsonPropertyName("iucr")]
        public string IUCR { get; set; }

        [JsonPropertyName("primary_type")]
        public string PrimaryType { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("location_description")]
        public string LocationDescription { get; set; }

        [JsonPropertyName("arrest")]
        public bool Arrest { get; set; }

        [JsonPropertyName("domestic")]
        public bool Domestic { get; set; }

        [JsonPropertyName("beat")]
        public int Beat { get; set; }

        [JsonPropertyName("district")]
        public int District { get; set; }

        [JsonPropertyName("ward")]
        public int Ward { get; set; }

        [JsonPropertyName("community_area")]
        public int CommunityArea { get; set; }

        [JsonPropertyName("fbi_code")]
        public int FbiCode { get; set; }

        [JsonPropertyName("x_coordinate")]
        public int XCoordinate { get; set; }

        [JsonPropertyName("y_coordinate")]
        public int YCoordinate { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("updated_on")]
        public DateTime UpdatedOn { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longtitude")]
        public double Longtitude { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

    }
}
