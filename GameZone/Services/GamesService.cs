using GameZone.Data;
using GameZone.Models;
using GameZone.ModelView;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webEnv;
        private readonly string _imagesPath;
        private readonly int _SizePage=3;

        public GamesService(ApplicationDbContext context,IWebHostEnvironment webEnv)
        {
            _context = context;
            _webEnv = webEnv;
            _imagesPath = $"{_webEnv.WebRootPath}/Assets/Images/Games";
        }
        public async Task CreateGame(GameFormCreate form)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(form.icon.FileName)}";
            var path = Path.Combine(_imagesPath,fileName);
            using var stream = File.Create(path) ;
            await form.icon.CopyToAsync(stream) ;

            Game game = new()
            {
                CategoryId= form.CategoryId,
                Name= form.Name,
                Description= form.Description,
                icon= fileName,
                Device = form.DevicesId.Select(p => new GameDevice { DeviceId = Convert.ToInt32(p)}).ToList()

            };
            _context.Add(game);
            _context.SaveChanges(); 

            
        }

        public async Task<bool> delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Remove(game);
                var done = _context.SaveChanges();
                if(done >0)
                {
                    var path = Path.Combine(_imagesPath, game.icon);
                    File.Delete(path);
                    return true;
                }

                return false;

            }
            return false;
        }

        public IEnumerable<Game> GetAll()
        {
            return _context.Games
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Device).ThenInclude(d => d.Device)
                .ToList();
        }

        public Game? getById(int id)
        {
            return _context.Games
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Device).ThenInclude(d => d.Device)
                .SingleOrDefault(g => g.Id ==id);
                
        }

        public IEnumerable<Game> GetGamesByPages(int pageIndex)
        {
            int pageSize = _SizePage;
            var games = _context.Games
                .Include(p => p.Category)
                .Include(p => p.Device).ThenInclude(d => d.Device)
                .Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return games;
        }

        public IEnumerable<Game> ShearchByName(string Name)
        {
            var games = _context.Games
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.Device).ThenInclude(d => d.Device)
                .Where(p => p.Name.Contains(Name))
               .ToList();
            return games;



        }

        public int TotalGamesPage()
        {
            

            double number = _context.Games.ToList().Count();
            int i = Convert.ToInt32( Math.Ceiling(number/_SizePage));
            return i;
        }

        public async Task<Game?> Update(GameFormEdit form)
        {
            var game = await _context.Games.Include(p => p.Device).SingleOrDefaultAsync(p => p.Id == form.Id);
            if (game == null)
            {
                return null;
            }
            var old_name_icon = game.icon;
            game.CategoryId = form.CategoryId;
            game.Name = form.Name;
            game.Description = form.Description;
            game.Device=form.DevicesId.Select(p => new GameDevice { DeviceId=p}).ToList();

            var isChanged = form.icon is not null;
            if (isChanged)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(form.icon.FileName)}";
                var path = Path.Combine(_imagesPath, fileName);
                using var stream = File.Create(path);
                await form.icon.CopyToAsync(stream);
                game.icon = fileName;               

            }
            var valid = await _context.SaveChangesAsync();
            if(valid > 0)
            {
                if(isChanged)
                {
                    var path = Path.Combine(_imagesPath, old_name_icon);
                    File.Delete(path);
                    
                }
                return game;
            }
            return null;
            
        }
    }
}
