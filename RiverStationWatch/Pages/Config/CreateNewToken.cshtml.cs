using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;
using RiverStationWatch.Services;

namespace RiverStationWatch.Pages.Config
{
    public class CreateNewTokenModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateNewTokenModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [TempData]
        public string SuccessMessage { get; set; }

        public string CurrentToken { get; set; }

        public async Task OnGetAsync()
        {
            // Load the current valid token from the database
            CurrentToken = await LoadCurrentTokenAsync();
        }

        [BindProperty]
        public ApiToken ApiToken { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.StationTokens == null || ApiToken == null)
            {
                ModelState.AddModelError("", "There were errors during saving the new token.");
                return Page();
            }

            _context.StationTokens.Add(ApiToken);
            await _context.SaveChangesAsync();

            // Message on a page that the token generation was successfull
            SuccessMessage = "Token successfully added to the database.";

            return Page();
        }

        private async Task<string> LoadCurrentTokenAsync()
        {
            // Load the most recent token from the database
            var latestToken = await _context.StationTokens
                                                .OrderByDescending(t => t.Validity) // Order by Validity in descending order
                                                .LastOrDefaultAsync();
            return latestToken?.Token;
        }

        public async Task<IActionResult> OnPostGetNewTokenAsync()
        {
            TokenGenerator tknGen = new TokenGenerator();
            // Generate a new token
            string newToken = tknGen.GenerateTokenStr();

            if (string.IsNullOrEmpty(newToken))
            {
                // If token generation failed, returns a BadRequestResult
                return BadRequest();
            }

            // If token generation succeeded, returns an OkObjectResult with the token string
            return new OkObjectResult(newToken);
        }
    }
}
