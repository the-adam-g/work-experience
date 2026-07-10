using HarryPotter.Server.Models;
using HarryPotter.Client.Core.Models;
using System.Text.Json;

namespace HarryPotter.Client.Core.Services
{
    public class HarryPotterService
    {
        private readonly HttpClient _httpClient;

        private const string BaseAddress = "harrypotter/";

        public HarryPotterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{BaseAddress}characters");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Character>? characters = JsonSerializer.Deserialize<List<Character>>(json);

            return characters ?? new List<Character>();
        }

        public async Task<List<Spell>> GetSpellsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{BaseAddress}spells"); //defines url for the api request + page
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Spell>? spells = JsonSerializer.Deserialize<List<Spell>>(json);

            return spells ?? new List<Spell>();
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://potterapi-fedeperin.vercel.app/en/books"); //defines url for the api request + page
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Book>? books = JsonSerializer.Deserialize<List<Book>>(json);

            return books ?? new List<Book>();
        }
        public async Task<List<Potion>> GetPotionsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://api.potterdb.com/v1/potions"); //defines url for the api request + page
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            // The PotterDB API wraps results in a "data" property: { "data": [ ... ] }
            try
            {
                using JsonDocument doc = JsonDocument.Parse(json);
                if (doc.RootElement.ValueKind == JsonValueKind.Object && doc.RootElement.TryGetProperty("data", out var dataElement))
                {
                    List<Potion>? potions = JsonSerializer.Deserialize<List<Potion>>(dataElement.GetRawText());
                    return potions ?? new List<Potion>();
                }
                else
                {
                    List<Potion>? potions = JsonSerializer.Deserialize<List<Potion>>(json);
                    return potions ?? new List<Potion>();
                }
            }
            catch (JsonException)
            {
                // If deserialization fails, return empty list instead of throwing to allow UI to handle gracefully
                return new List<Potion>();
            }
        }
    }
}
