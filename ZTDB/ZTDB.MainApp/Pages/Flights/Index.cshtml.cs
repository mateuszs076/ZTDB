using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZTDB.SQLDatabase;
using ZTDB.SQLDatabase.Models;

namespace ZTDB.MainApp.Pages.Flights
{
    public class IndexModel : PageModel
    {
        private readonly SQLContext _context;
        private readonly Stopwatch timer;

        /// <summary>
        /// Lista przechowująca dane do wyświetlenia na froncie
        /// </summary>
        public IList<Flight> Flight { get; set; }

        /// <summary>
        /// Czas operacji na bazie do wyświetlenia na froncie
        /// </summary>
        public TimeSpan OperationTIme { get; set; }

        public IndexModel(SQLContext context)
        {
            timer = new Stopwatch();
            _context = context;
        }

        public async Task OnGetAsync()
        {
            timer.Restart();

            //pobranie lotów o id między 10 000 a 10 010 i dodanie wyszystkich relacji
            Flight = await _context.Flight
                .Where(a => a.Id > 10_000 && a.Id < 10_010)
                .Include(f => f.Airline)
                .Include(f => f.CancelCode)
                .Include(f => f.DestinationLocation)
                .Include(f => f.OriginLocation)
                .ToListAsync();

            timer.Stop();
            OperationTIme = timer.Elapsed;
        }
    }
}