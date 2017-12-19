using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key, Column("Id")]
        public int ReservationsNr { get; set; }
        
        public int AutoId { get; set; }

        [ForeignKey("AutoId")]
        public virtual Auto Auto { get; set; }
        public int KundeId { get; set; }
        public virtual Kunde Kunde { get; set; }
        [Required]
        public DateTime Von { get; set; }
        [Required]
        public DateTime Bis { get; set; }
        [Timestamp] 
        public byte[] RowVersion { get; set; }
    }
}
