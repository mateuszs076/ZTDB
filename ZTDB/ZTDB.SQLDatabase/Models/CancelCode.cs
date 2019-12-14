using System.ComponentModel.DataAnnotations;

namespace ZTDB.SQLDatabase.Models
{
    public class CancelCode
    {
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }
    }
}