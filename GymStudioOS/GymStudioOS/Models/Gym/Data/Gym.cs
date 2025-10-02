using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymStudioOS.Data;

namespace GymStudioOS.Models.Gym.Data
{
    public class Gym
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string OwnerId { get; set; } = string.Empty;
        [ForeignKey(nameof(OwnerId))]
        public ApplicationUser? Owner { get; set; }
    }
}