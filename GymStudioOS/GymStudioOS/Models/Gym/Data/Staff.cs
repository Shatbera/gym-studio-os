using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymStudioOS.Data;

namespace GymStudioOS.Models.Gym.Data
{
	public class Staff
	{
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; } = string.Empty;
		[Required]
		public string LastName { get; set; } = string.Empty;
		[Required]
		public string Role { get; set; } = string.Empty;
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Phone { get; set; } = string.Empty;

		[Required]
		public int GymId { get; set; }
		[ForeignKey(nameof(GymId))]
		public Gym? Gym { get; set; }

		public string ApplicationUserId { get; set; } = string.Empty;
		[ForeignKey(nameof(ApplicationUserId))]
		public ApplicationUser? ApplicationUser { get; set; }
	}
}
