using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;
using RiverStationWatch.Pages.Shared;

namespace RiverStationWatch.Pages.StationRecordPages
{
    [Authorize]
    public class StationRecordListModel : PageModel
    {
        private const byte PageSize = 20;
        private readonly ApplicationDbContext _context;

        public StationRecordListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Record> RecordList { get; set; } = default!;
        public bool IsFloodLevelExceeded { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            RecordList = await _context.Records
                                     .Include(hv => hv.Station)
                                     .Take(20)
                                     .ToListAsync();

            var stationsWithExceededFloodLevel = await GetStationsWithExceededFloodLevel();

            ViewData["StationsWithExceededFloodLevel"] = stationsWithExceededFloodLevel;

            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }

        // Deletion from the DB
        public async Task<IActionResult> OnPostDeleteRecordAsync(Guid id)
        {
            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();

            // Redirect back to the same page after deletion
            return RedirectToPage();
        }

        // Searching from the DB
        public async Task<IActionResult> OnPostSearchAsync(string search)
        {
            var searchResults = await _context.Records
                .Include(hv => hv.Station)
                .Where(r => r.Station.StationName.Contains(search)) // Adjust the condition based on your search criteria
                                                                    //.Take(5)
                .ToListAsync();

            return Partial("_SearchRecordsPartial", searchResults);
        }

        // Loading more records
        public async Task<IActionResult> OnGetLoadMoreAsync(int offset)
        {
            var moreRecords = await _context.Records
                .Include(hv => hv.Station)
                //.OrderByDescending(r => r.TimeStamp)
                .Skip(offset) // Skip the already loaded records
                .Take(PageSize) // Load additional records
                .ToListAsync();

            //var model = new _MoreRecordsPartialModel(_context); // Instantiate the model
            //model.Records = moreRecords; // Set the Record property
            return Partial("_MoreRecordsPartial", moreRecords); // Pass the model to the partial view
        }

        // Flood level exceedance check for getting the names of stations
        public async Task<List<string>> GetStationsWithExceededFloodLevel()
        {
            var stationsWithExceededFloodLevel = new List<string>();

            if (_context.Records.Any())
            {
                var stations = _context.Stations.ToList();

                foreach (var station in stations)
                {
                    var records = _context.Records
                        .Where(r => r.StationId == station.Id && r.Value >= station.FloodLevel)
                        .ToList();

                    if (records.Count >= 3)
                    {
                        stationsWithExceededFloodLevel.Add(station.StationName);
                        IsFloodLevelExceeded = true;
                    }
                }
            }

            return stationsWithExceededFloodLevel;
        }

    }
}
