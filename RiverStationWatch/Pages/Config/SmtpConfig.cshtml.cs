using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Pages.Config
{
    public class SmtpConfigModel : PageModel
    {
        private readonly RiverStationWatch.Data.ApplicationDbContext _context;

        public SmtpConfigModel(RiverStationWatch.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SmtpConfig SmtpConfig { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.SmtpConfigs == null || SmtpConfig == null)
            {
                return Page();
            }

            _context.SmtpConfigs.Add(SmtpConfig);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
