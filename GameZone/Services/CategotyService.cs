using GameZone.Data;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategotyService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategotyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public  async Task<List<SelectListItem>> GetAllCategories()
        {
            return  _context.Categories.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).OrderBy(p => p.Text).AsNoTracking().ToList();


        }
    }
}
