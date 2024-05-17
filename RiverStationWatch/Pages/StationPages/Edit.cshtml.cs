using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Pages.StationPages
{
    public class EditModel : PageModel
    {
        private readonly RiverStationWatch.Data.ApplicationDbContext _context;

        public EditModel(RiverStationWatch.Data.ApplicationDbContext context)
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

            var station =  await _context.Stations.FirstOrDefaultAsync(m => m.Id == id);
            if (station == null)
            {
                return NotFound();
            }
            Station = station;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if FloodLevel is higher than DroughtLevel
            if (Station.FloodLevel <= Station.DroughtLevel)
            {
                ModelState.AddModelError("", "Flood level must be higher than drought level.");
                return Page();
            }

            _context.Attach(Station).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(Station.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StationExists(int id)
        {
          return (_context.Stations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
