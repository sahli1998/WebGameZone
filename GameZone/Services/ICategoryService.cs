using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.Services
{
    public interface ICategoryService
    {
        public Task<List<SelectListItem>> GetAllCategories();
    }
}
