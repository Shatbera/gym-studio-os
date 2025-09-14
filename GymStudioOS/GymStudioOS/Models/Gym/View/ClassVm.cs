using System.ComponentModel.DataAnnotations;

namespace GymStudioOS.Models.Gym.View
{
    public class ClassVm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int DefaultDurationMinutes { get; set; }
    }
}
