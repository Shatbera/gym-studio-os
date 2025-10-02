using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Gym.View
{
    public class UserProfileVM
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(20)]
        public string Phone { get; set; }
        [Required, MaxLength(20)]
        public string PersonalId { get; set; }

        [MaxLength(500)]
        public string? Bio { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
