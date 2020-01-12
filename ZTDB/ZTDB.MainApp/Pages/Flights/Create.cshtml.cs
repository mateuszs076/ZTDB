using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZTDB.SQLDatabase;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.MainApp.Pages.Flights
{
    public class CreateModel : PageModel
    {
        private readonly ZTDB.SQLDatabase.SQLContext _context;

        public CreateModel(ZTDB.SQLDatabase.SQLContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AirlineId"] = new SelectList(_context.Airline, "Id", "Id");
        ViewData["CancelCodeId"] = new SelectList(_context.CancelCode, "Id", "Id");
        ViewData["DestinationLocationId"] = new SelectList(_context.Location, "Id", "Id");
        ViewData["OriginLocationId"] = new SelectList(_context.Location, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Flight Flight { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Flight.Add(Flight);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
