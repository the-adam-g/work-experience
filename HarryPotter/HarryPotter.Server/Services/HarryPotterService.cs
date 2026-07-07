using System.Text.Json;
using HarryPotter.Server.Models;
using HarryPotter.Server.Interfaces;
using HarryPotter.Client.Pages;

namespace HarryPotter.Server.Services
{
    public class HarryPotterService : IHarryPotterService
    {
        private readonly HttpClient _httpClient;

        public HarryPotterService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<List<Character>> GetCharactersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("characters");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Character>? characters = JsonSerializer.Deserialize<List<Character>>(json); //interpret json from api call

            return characters ?? new List<Character>();
        }
        
        public async Task<List<Spell>> GetSpellsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("spells");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Spell>? spells = JsonSerializer.Deserialize<List<Spell>>(json); //interpret json from api call

            return spells ?? new List<Spell>();
        }
        public async Task<List<Book>> GetBooksAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("books");
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            List<Book>? books = JsonSerializer.Deserialize<List<Book>>(json); //interpret json from api call

            return books ?? new List<Book>();
        }
    }
}
