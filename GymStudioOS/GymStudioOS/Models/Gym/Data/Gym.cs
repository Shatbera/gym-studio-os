using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Gym.Data
{
    public class Gym
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}