using System.ComponentModel.DataAnnotations;

namespace SimpleCRM.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(255)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public string Status { get; set; } = "Новое"; // Начальное значение

        public string? ResponsibleEmployee { get; set; }
    }
}