using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymStudioOS.Data;

namespace GymStudioOS.Models.Gym.Data
{
    public class GymUserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GymId { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym? Gym { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Optional: Branch assignment (nullable, for gym-wide roles)
        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public GymBranch? Branch { get; set; }
    }
}
