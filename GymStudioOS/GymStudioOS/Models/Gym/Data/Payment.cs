using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GymStudioOS.Models.Gym.Data
{
	public class Payment
	{
		[Key]
		public int Id { get; set; }

		public int MemberId { get; set; }
		[ForeignKey(nameof(MemberId))]
		public Member? Member { get; set; }

		public decimal Amount { get; set; }

		public DateTime PaymentDate { get; set; }
	}
}
