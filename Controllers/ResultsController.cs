
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Filters;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            var results = await _context.Results.ToListAsync();

            return Ok(results);
        }

        // GET: api/Results/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Result>> GetResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Result not found."
                });
            }

            return Ok(result);
        }

        // POST: api/Results
        [HttpPost]
        [SessionAuthorize("Organiser")]
        public async Task<ActionResult<Result>> CreateResult(Result result)
        {
            result.ResultID = 0;

            _context.Results.Add(result);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetResult),
                new { id = result.ResultID },
                result);
        }

        // PUT: api/Results/1
        [HttpPut("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> UpdateResult(
            int id, Result result)
        {
            if (id != result.ResultID)
            {
                return BadRequest(new
                {
                    message = "The result ID does not match."
                });
            }

            var existingResult = await _context.Results.FindAsync(id);

            if (existingResult == null)
            {
                return NotFound(new
                {
                    message = "Result not found."
                });
            }

            _context.Entry(existingResult).CurrentValues.SetValues(result);

            await _context.SaveChangesAsync();

            return Ok(existingResult);
        }

        // DELETE: api/Results/1
        [HttpDelete("{id}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Result not found."
                });
            }

            _context.Results.Remove(result);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Result deleted successfully."
            });
        }
    }
}
