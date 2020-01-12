using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZTDB.MongoDatabase;
using ZTDB.MongoDatabase.Models;

namespace ZTDB.MainApp.Pages.Mongo.Flights
{
    public class IndexModel : PageModel
    {
        private static MongoContext _context = null;
        private readonly Stopwatch timer;

        /// <summary>
        /// Lista przechowująca dane do wyświetlenia na froncie
        /// </summary>
        public IList<Flight> Flight { get; set; }

        /// <summary>
        /// Czas operacji na bazie do wyświetlenia na froncie
        /// </summary>
        public TimeSpan OperationTIme { get; set; }

        public IndexModel()
        {
            if (_context == null)
                _context = new MongoContext();
            timer = new Stopwatch();
        }

        public void OnGet()
        {
            timer.Restart();

            //pobranie 10 lotów
            Flight = _context.Get(10);

            timer.Stop();
            OperationTIme = timer.Elapsed;
        }
    }
}