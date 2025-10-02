using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Gym.View
{
    public class GymBranchVm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public int GymId { get; set; }
    }
}