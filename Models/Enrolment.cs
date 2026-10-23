
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class Enrolment
    {
        [Key]
        public int EnrolmentID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        public DateTime EnrolmentDate { get; set; }
            = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        [Required]
        [StringLength(20)]
        public string PaymentStatus { get; set; } = "Unpaid";

        // Participant who entered the event
        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;

        // Event selected by the participant
        [ForeignKey(nameof(EventID))]
        public Event Event { get; set; } = null!;

        // Category selected by the participant
        [ForeignKey(nameof(CategoryID))]
        public EventCategory Category { get; set; } = null!;

        // Race result, once available
        public Result? Result { get; set; }
    }
}
