using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ZTDB.SQLDatabase;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.MainApp.Pages.Flights
{
    public class DetailsModel : PageModel
    {
        private readonly ZTDB.SQLDatabase.SQLContext _context;

        public DetailsModel(ZTDB.SQLDatabase.SQLContext context)
        {
            _context = context;
        }

        public Flight Flight { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flight = await _context.Flight
                .Include(f => f.Airline)
                .Include(f => f.CancelCode)
                .Include(f => f.DestinationLocation)
                .Include(f => f.OriginLocation).FirstOrDefaultAsync(m => m.Id == id);

            if (Flight == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
