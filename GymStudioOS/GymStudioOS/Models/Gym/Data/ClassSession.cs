using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using GymStudioOS.Data;

namespace GymStudioOS.Models.Gym.Data
{
    public class ClassSession
    {
        [Key]
        public int Id { get; set; }

        public int ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class? Class { get; set; }

        public int TrainerId { get; set; }
        [ForeignKey(nameof(TrainerId))]
        public ApplicationUser? Trainer { get; set; }

        
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; } 

        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
