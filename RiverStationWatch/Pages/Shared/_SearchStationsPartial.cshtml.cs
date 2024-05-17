using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;
using System.Linq;
using System.Threading.Tasks;

namespace RiverStationWatch.Pages.Shared
{
    public class _SearchStationsPartialModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public _SearchStationsPartialModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Station> Stations { get; set; }

        public async Task<IActionResult> OnGetAsync(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                Stations = await _context.Stations
                    .Where(s => s.StationName.Contains(search))
                    .ToListAsync();
            }
            else
            {
                Stations = new List<Station>();
            }

            return Page();
        }
    }
}
