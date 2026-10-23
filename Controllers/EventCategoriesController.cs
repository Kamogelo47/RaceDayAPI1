
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Filters;
using RaceDayAPI.Models;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventCategoriesController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public EventCategoriesController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/EventCategories
        // View all event categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.EventCategories
                .AsNoTracking()
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/EventCategories/5
        // View one event category
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.EventCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryID == id);

            if (category == null)
            {
                return NotFound(new
                {
                    Message = "Event category not found."
                });
            }

            return Ok(category);
        }

        // POST: api/EventCategories
        // Organisers only: create a category
        [HttpPost]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> CreateCategory(
            [FromBody] EventCategory category)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            _context.EventCategories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.CategoryID },
                new
                {
                    Message = "Event category created successfully.",
                    Category = category
                });
        }

        // PUT: api/EventCategories/5
        // Organisers only: update a category
        [HttpPut("{id:int}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> UpdateCategory(
            int id,
            [FromBody] EventCategory updatedCategory)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var category = await _context.EventCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    Message = "Event category not found."
                });
            }

            // Update the existing category's values
            _context.Entry(category).CurrentValues.SetValues(updatedCategory);

            // Ensure the URL ID remains the category's ID
            category.CategoryID = id;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Event category updated successfully.",
                Category = category
            });
        }

        // DELETE: api/EventCategories/5
        // Organisers only: delete a category
        [HttpDelete("{id:int}")]
        [SessionAuthorize("Organiser")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.EventCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    Message = "Event category not found."
                });
            }

            _context.EventCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Event category deleted successfully."
            });
        }
    }
}
