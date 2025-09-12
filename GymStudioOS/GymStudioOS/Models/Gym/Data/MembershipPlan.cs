using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymStudioOS.Models.Gym.Data
{
    public class MembershipPlan
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int DurationMonths { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int GymId { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym? Gym { get; set; }
    }
}
