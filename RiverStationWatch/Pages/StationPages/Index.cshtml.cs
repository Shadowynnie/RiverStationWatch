using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Pages.StationPages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Station> Station { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Stations != null)
            {
                Station = await _context.Stations.ToListAsync();
            }
            
        }

        public async Task<IActionResult> OnPostSearchAsync(string search)
        {
            var searchResults = await _context.Stations
                .Where(s => s.StationName.Contains(search))
                .ToListAsync();

            return Partial("_SearchStationsPartial", searchResults);
        }
    }
}
