using System.Text.Json.Serialization;

namespace fin_backend.Models.DTOs
{
    public class NewsItemDTO
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("datetime")]
        public long Datetime { get; set; }

        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("id")]
        public long FinnhubId { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("related")]
        public string Related { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
