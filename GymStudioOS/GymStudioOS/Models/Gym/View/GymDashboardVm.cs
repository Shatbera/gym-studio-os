using System.ComponentModel.DataAnnotations;
using GymStudioOS.Models.Gym.Data;

namespace GymStudioOS.Models.Gym.View
{
    public class GymDashboardVm
    {
        [Required]
        public Data.Gym? Gym { get; set; }
        [Required]
        public IEnumerable<Class>? Classes { get; set; }
    }
}
