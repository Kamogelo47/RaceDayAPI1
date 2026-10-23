
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("Results")]
    public class Result
    {
        [Key]
        public int ResultID { get; set; }

        [Required]
        public int EnrolmentID { get; set; }

        // Participant's finishing time
        [Column(TypeName = "time")]
        public TimeSpan? FinishTime { get; set; }

        // Actual recorded race time
        [Column(TypeName = "time")]
        public TimeSpan? NetTime { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        [Range(1, int.MaxValue)]
        public int Rank { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        // Connect the result to its enrolment
        [ForeignKey(nameof(EnrolmentID))]
        public Enrolment Enrolment { get; set; } = null!;
    }
}
