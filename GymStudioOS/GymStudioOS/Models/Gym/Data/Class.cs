using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymStudioOS.Models.Gym.Data
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int DefaultDurationMinutes { get; set; }

        [Required]
        public int GymId { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym? Gym { get; set; }
    }
}
