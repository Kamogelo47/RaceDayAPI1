
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDayAPI.Models
{
    [Table("EventCourseDetails")]
    public class EventCourseDetail
    {
        [Key]
        public int CourseID { get; set; }

        [Required]
        public int EventID { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string RouteName { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal DistanceKm { get; set; }

        [Required]
        public int ElevationGain { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? WeatherInfo { get; set; }

        // Relationship with the Events table
        [ForeignKey(nameof(EventID))]
        public Event Event { get; set; } = null!;
    }
}
