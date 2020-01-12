using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ZTDB.MongoDatabase;
using ZTDB.MongoDatabase.Models;

namespace ZTDB.MainApp.Pages.Mongo.Flights
{
    public class IndexModel : PageModel
    {
        private static MongoContext _context = null;
        private readonly Stopwatch timer;
        public IList<Flight> Flight { get; set; }
        public TimeSpan OperationTIme { get; set; }

        public IndexModel()
        { 
            if(_context == null)
                _context = new MongoContext();
            timer = new Stopwatch();
        }


        public void OnGet()
        {
            timer.Restart();
            Flight =  _context.Get(10);
            timer.Stop();
            OperationTIme = timer.Elapsed;
        }
    }
}