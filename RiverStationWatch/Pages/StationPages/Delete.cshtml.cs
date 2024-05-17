using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Pages.StationPages
{
    public class DeleteModel : PageModel
    {
        private readonly RiverStationWatch.Data.ApplicationDbContext _context;

        public DeleteModel(RiverStationWatch.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Station Station { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Stations == null)
            {
                return NotFound();
            }

            var station = await _context.Stations.FirstOrDefaultAsync(m => m.Id == id);

            if (station == null)
            {
                return NotFound();
            }
            else 
            {
                Station = station;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Stations == null)
            {
                return NotFound();
            }
            var station = await _context.Stations.FindAsync(id);

            if (station != null)
            {
                Station = station;
                _context.Stations.Remove(Station);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
