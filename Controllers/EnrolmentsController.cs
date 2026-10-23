
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Filters;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EnrolmentsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrolments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrolment>>> GetEnrolments()
        {
            var enrolments = await _context.Enrolments.ToListAsync();

            return Ok(enrolments);
        }

        // GET: api/Enrolments/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Enrolment>> GetEnrolment(int id)
        {
            var enrolment = await _context.Enrolments
                .FindAsync(id);

            if (enrolment == null)
            {
                return NotFound(new
                {
                    message = "Enrolment not found."
                });
            }

            return Ok(enrolment);
        }

        // POST: api/Enrolments
        [HttpPost]
        [SessionAuthorize("Participant")]
        public async Task<ActionResult<Enrolment>> CreateEnrolment(
            Enrolment enrolment)
        {
            enrolment.EnrolmentID = 0;

            if (enrolment.EnrolmentDate == default)
            {
                enrolment.EnrolmentDate = DateTime.Today;
            }

            if (string.IsNullOrWhiteSpace(enrolment.Status))
            {
                enrolment.Status = "Active";
            }

            if (string.IsNullOrWhiteSpace(enrolment.PaymentStatus))
            {
                enrolment.PaymentStatus = "Pending";
            }

            _context.Enrolments.Add(enrolment);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEnrolment),
                new { id = enrolment.EnrolmentID },
                enrolment);
        }

        // PUT: api/Enrolments/1
        [HttpPut("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> UpdateEnrolment(
            int id, Enrolment enrolment)
        {
            if (id != enrolment.EnrolmentID)
            {
                return BadRequest(new
                {
                    message = "The enrolment ID does not match."
                });
            }

            var existingEnrolment = await _context.Enrolments
                .FindAsync(id);

            if (existingEnrolment == null)
            {
                return NotFound(new
                {
                    message = "Enrolment not found."
                });
            }

            existingEnrolment.UserID = enrolment.UserID;
            existingEnrolment.EventID = enrolment.EventID;
            existingEnrolment.CategoryID = enrolment.CategoryID;
            existingEnrolment.EnrolmentDate = enrolment.EnrolmentDate;
            existingEnrolment.Status = enrolment.Status;
            existingEnrolment.PaymentStatus = enrolment.PaymentStatus;

            await _context.SaveChangesAsync();

            return Ok(existingEnrolment);
        }

        // DELETE: api/Enrolments/1
        [HttpDelete("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> DeleteEnrolment(int id)
        {
            var enrolment = await _context.Enrolments
                .FindAsync(id);

            if (enrolment == null)
            {
                return NotFound(new
                {
                    message = "Enrolment not found."
                });
            }

            _context.Enrolments.Remove(enrolment);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Enrolment deleted successfully."
            });
        }
    }
}
