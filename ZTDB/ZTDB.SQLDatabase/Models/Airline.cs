using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZTDB.SQLDatabase.Models
{
    [Table("Airlines")]
    public class Airline
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        public virtual List<Flight> Flights { get; set; }
    }
}