using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymStudioOS.Models.Gym.Data
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public DateTime JoinDate { get; set; }

        [Required]
        public int GymId { get; set; }
        [ForeignKey(nameof(GymId))]
        public Gym? Gym { get; set; }
    }
}
