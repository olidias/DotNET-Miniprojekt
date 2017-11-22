using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AutoId { get; set; }
        [Required]
        public int KundeId { get; set; }
        [Required]
        public DateTime Von { get; set; }
        [Required]
        public DateTime Bis { get; set; }
        [Timestamp] 
        public byte[] RowVersion { get; set; }
    }
}
