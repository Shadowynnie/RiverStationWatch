using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverStationWatch.Data.Model;
using RiverStationWatch.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace RiverStationWatch.Pages.StationRecordPages
{
    [Authorize]
    public class RecordChartModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RecordChartModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? StationId { get; set; }

        public List<Station> Stations { get; set; }

        public List<DateTime> TimeStamps { get; set; }

        public List<int> WaterLevels { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Stations = await _context.Stations.ToListAsync();

            if (!StationId.HasValue && Stations.Any())
            {
                StationId = Stations.First().Id; // Default to the first station if no station is selected
            }

            if (StationId.HasValue)
            {
                var records = await _context.Records
                    .Where(r => r.StationId == StationId)
                    .OrderBy(r => r.TimeStamp)
                    .ToListAsync();

                TimeStamps = records.Select(r => r.TimeStamp).ToList();
                WaterLevels = records.Select(r => r.Value).ToList();
            }

            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }
    }
}
