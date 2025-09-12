using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymStudioOS.Models.Gym.Data
{
    public class MemberMembership
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }

        [Required]
        public int MembershipPlanId { get; set; }
        [ForeignKey(nameof(MembershipPlanId))]
        public MembershipPlan? MembershipPlan { get; set; }

    }
}
