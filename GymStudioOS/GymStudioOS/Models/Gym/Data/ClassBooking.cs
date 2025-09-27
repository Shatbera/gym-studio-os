using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymStudioOS.Data;
namespace GymStudioOS.Models.Gym.Data
{
	public class ClassBooking
	{
		[Key]
		public int Id { get; set; }

		public int ClassSessionId { get; set; }
		[ForeignKey(nameof(ClassSessionId))]
		public ClassSession? ClassSession { get; set; }

		public int MemberId { get; set; }
		[ForeignKey(nameof(MemberId))]
		public ApplicationUser? Member { get; set; }

		public DateTime BookingDate { get; set; }

		public bool IsCancelled { get; set; }
	}
}
