using System.Text.Json.Serialization;

namespace HarryPotter.Server.Models
{
    public class Potion
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("attributes")]
        public PotionAttributes? Attributes { get; set; }
    }

    public class PotionAttributes
    {
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("characteristics")]
        public string? Characteristics { get; set; }

        [JsonPropertyName("difficulty")]
        public string? Difficulty { get; set; }

        [JsonPropertyName("effect")]
        public string? Effect { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("inventors")]
        public string? Inventors { get; set; }

        [JsonPropertyName("ingredients")]
        public string? Ingredients { get; set; }

        [JsonPropertyName("manufacturers")]
        public string? Manufacturers { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("side_effects")]
        public string? SideEffects { get; set; }

        [JsonPropertyName("time")]
        public string? Time { get; set; }

        [JsonPropertyName("wiki")]
        public string? Wiki { get; set; }
    }
}