using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ApplicationDbContext _context;
        public DeviceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SelectListItem>> GetAllDevices()
        {
            return  _context.Devices.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).OrderBy(p => p.Text).AsNoTracking().ToList();
        }
    }
}
