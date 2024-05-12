using System.Text.Json.Serialization;

namespace NetwiseApp.Models
{
    public class CatFactResponse
    {
        [JsonPropertyName("fact")]
        public required string Fact { get; set; }
        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}
