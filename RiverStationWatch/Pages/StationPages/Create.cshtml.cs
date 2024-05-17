using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Pages.StationPages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            return Page();
        }

        [BindProperty]
        public Station Station { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Stations == null || Station == null)
            {
                return Page();
            }

            // Check if FloodLevel is higher than DroughtLevel
            if (Station.FloodLevel <= Station.DroughtLevel)
            {
                ModelState.AddModelError("", "Flood level must be higher than drought level.");
                return Page();
            }

            _context.Stations.Add(Station);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
