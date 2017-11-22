using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Auto
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Marke { get; set; }
        [Required]
        public int Tagestarif { get; set; }
        [Timestamp, Required]
        public byte[] RowVersion { get; set; }
        public int Basistarif { get; set; }
        [Required]
        public int AutoKlasse { get; set; }
    }
 
}
