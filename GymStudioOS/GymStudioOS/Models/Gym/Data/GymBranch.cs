using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymStudioOS.Models.Gym.Data
{
    public class GymBranch
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [ForeignKey("Gym")]
        public int GymId { get; set; }
        public Gym? Gym { get; set; }
    }
}