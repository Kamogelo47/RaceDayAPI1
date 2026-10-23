
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal DistanceKm { get; set; }

        [Required]
        [StringLength(10)]
        public string EventType { get; set; } = "Run";

        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
            = DateTime.UtcNow;

        [ForeignKey(nameof(CreatedBy))]
        public User Creator { get; set; } = null!;

        public ICollection<EventCategory> Categories { get; set; }
            = new List<EventCategory>();

        public ICollection<Enrolment> Enrolments { get; set; }
            = new List<Enrolment>();

        public ICollection<EventCourseDetail> CourseDetails { get; set; }
            = new List<EventCourseDetail>();
    }
}
