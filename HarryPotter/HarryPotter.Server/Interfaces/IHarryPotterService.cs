using HarryPotter.Server.Models;

namespace HarryPotter.Server.Interfaces
{
    public interface IHarryPotterService
    {
        Task<List<Character>> GetCharactersAsync();
        Task<List<Spell>> GetSpellsAsync();
        Task<List<Book>> GetBooksAsync();

        Task<List<Potion>> GetPotionsAsync();
    }
}
