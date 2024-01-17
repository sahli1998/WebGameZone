using GameZone.Models;
using GameZone.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ModelView
{
    public class GameFormCreate
    {
        
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name= "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoriesList { get; set; } = Enumerable.Empty<SelectListItem>();

        public List<int> DevicesId { get; set; }

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [ValidationAttributeExtensions(Settings.SettingFile._allowedExtensions), ValidationSize(Settings.SettingFile._maxInBytes)]
        public IFormFile icon { get; set; } 



    }
}
