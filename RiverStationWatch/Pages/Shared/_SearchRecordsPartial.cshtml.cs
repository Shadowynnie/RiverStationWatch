using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverStationWatch.Data;
using RiverStationWatch.Data.Model;

namespace RiverStationWatch.Pages.Shared
{
    public class _RecordListPartialModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public _RecordListPartialModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Record> Record { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Records != null)
            {
                Record = await _context.Records
                .Include(r => r.Station)
                .ToListAsync();
            }
        }
    }
}