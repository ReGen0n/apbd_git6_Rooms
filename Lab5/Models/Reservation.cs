using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [StringLength(100)]
        public string OrganizerName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}