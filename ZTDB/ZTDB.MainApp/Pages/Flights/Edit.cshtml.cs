using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZTDB.SQLDatabase;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.MainApp.Pages.Flights
{
    public class EditModel : PageModel
    {
        private readonly ZTDB.SQLDatabase.SQLContext _context;

        public EditModel(ZTDB.SQLDatabase.SQLContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["AirlineId"] = new SelectList(_context.Airline, "Id", "Id");
           ViewData["CancelCodeId"] = new SelectList(_context.CancelCode, "Id", "Id");
           ViewData["DestinationLocationId"] = new SelectList(_context.Location, "Id", "Id");
           ViewData["OriginLocationId"] = new SelectList(_context.Location, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(Flight.Id))
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

        private bool FlightExists(int id)
        {
            return _context.Flight.Any(e => e.Id == id);
        }
    }
}
