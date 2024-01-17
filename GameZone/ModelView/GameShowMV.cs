using GameZone.Models;

namespace GameZone.ModelView
{
    public class GameShowMV
    {
        public IEnumerable<Game> GameList { get; set; }
        public int TotalePageNumber { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
