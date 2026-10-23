
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Filters;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventCourseDetailsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventCourseDetailsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/EventCourseDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventCourseDetail>>>
            GetEventCourseDetails()
        {
            var details = await _context.EventCourseDetails
                .AsNoTracking()
                .ToListAsync();

            return Ok(details);
        }

        // GET: api/EventCourseDetails/1
        [HttpGet("{id}")]
        public async Task<ActionResult<EventCourseDetail>>
            GetEventCourseDetail(int id)
        {
            var detail = await _context.EventCourseDetails
                .FindAsync(id);

            if (detail == null)
            {
                return NotFound(new
                {
                    message = "Event course details not found."
                });
            }

            return Ok(detail);
        }

        // POST: api/EventCourseDetails
        [HttpPost]
        [SessionAuthorize("Organiser")]
        public async Task<ActionResult<EventCourseDetail>>
            CreateEventCourseDetail(EventCourseDetail detail)
        {
            detail.CourseID = 0;

            _context.EventCourseDetails.Add(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEventCourseDetail),
                new { id = detail.CourseID },
                detail);
        }

        // PUT: api/EventCourseDetails/1
        [HttpPut("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> UpdateEventCourseDetail(
            int id, EventCourseDetail detail)
        {
            if (id != detail.CourseID)
            {
                return BadRequest(new
                {
                    message = "The course ID does not match."
                });
            }

            var existingDetail = await _context.EventCourseDetails
                .FindAsync(id);

            if (existingDetail == null)
            {
                return NotFound(new
                {
                    message = "Event course details not found."
                });
            }

            existingDetail.EventID = detail.EventID;
            existingDetail.RouteName = detail.RouteName;
            existingDetail.DistanceKm = detail.DistanceKm;
            existingDetail.ElevationGain = detail.ElevationGain;
            existingDetail.WeatherInfo = detail.WeatherInfo;

            await _context.SaveChangesAsync();

            return Ok(existingDetail);
        }

        // DELETE: api/EventCourseDetails/1
        [HttpDelete("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> DeleteEventCourseDetail(int id)
        {
            var detail = await _context.EventCourseDetails
                .FindAsync(id);

            if (detail == null)
            {
                return NotFound(new
                {
                    message = "Event course details not found."
                });
            }

            _context.EventCourseDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Event course details deleted successfully."
            });
        }
    }
}
