using GameZone.Models;
using GameZone.ModelView;

namespace GameZone.Services
{
    public interface IGamesService
    {
        public Task CreateGame(GameFormCreate form);
        public IEnumerable<Game> GetAll();

        public Task<Game?> Update(GameFormEdit form);

        public Game? getById(int id);

        public Task<bool> delete(int id);

        public IEnumerable<Game> GetGamesByPages(int pageIndex);

        public int TotalGamesPage();

        public IEnumerable<Game> ShearchByName(string Name);
    }
}
