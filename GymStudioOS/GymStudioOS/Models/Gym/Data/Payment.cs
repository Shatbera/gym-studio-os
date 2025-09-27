using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymStudioOS.Data;
namespace GymStudioOS.Models.Gym.Data
{
	public class Payment
	{
		[Key]
		public int Id { get; set; }

		public int UserId { get; set; }
		[ForeignKey(nameof(UserId))]
		public ApplicationUser? User { get; set; }

		public decimal Amount { get; set; }

		public DateTime PaymentDate { get; set; }
	}
}
