using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public abstract class Auto
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Marke { get; set; }
        [Required]
        public int Tagestarif { get; set; }
        [Timestamp, Required]
        public byte[] RowVersion { get; set; }
        [Required]
        public int AutoKlasse { get; set; }
        
        [InverseProperty("ReservationsNr")]
        public List<Reservation> Reservationen { get; set; }
    }
 
}
