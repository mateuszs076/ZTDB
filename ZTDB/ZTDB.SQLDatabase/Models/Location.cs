using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZTDB.SQLDatabase.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public virtual List<Flight> OriginFlights { get; set; }
        public virtual List<Flight> DestinationFlights { get; set; }
    }
}