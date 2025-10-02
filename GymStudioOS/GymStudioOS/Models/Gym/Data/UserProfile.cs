using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymStudioOS.Data;

namespace GymStudioOS.Models.Gym.Data
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }
        

        [Required, MaxLength(20)]
        public string Phone { get; set; }
        [Required, MaxLength(20)]
        public string PersonalId { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }

    }
}
