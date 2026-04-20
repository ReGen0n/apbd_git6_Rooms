using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string BuildingCode { get; set; } = string.Empty;

        [Range(0, 100)]
        public int Floor { get; set; }

        [Range(1, 1000)]
        public int Capacity { get; set; }

        public bool HasProjector { get; set; }

        public bool IsActive { get; set; }
    }
}