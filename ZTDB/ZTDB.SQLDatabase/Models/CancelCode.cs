using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZTDB.SQLDatabase.Models
{
    public class CancelCode
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public virtual List<Flight> Flights { get; set; }
    }
}