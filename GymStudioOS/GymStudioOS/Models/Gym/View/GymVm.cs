using GymStudioOS.Models.Gym.Data;

namespace GymStudioOS.Models.Gym.View
{
	public class GymVm
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string OwnerId { get; set; } = string.Empty;
	}
}
