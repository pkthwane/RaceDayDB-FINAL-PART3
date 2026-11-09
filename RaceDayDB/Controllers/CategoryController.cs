using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;
using Microsoft.AspNetCore.Authorization;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public CategoryController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public IActionResult AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.CategoryID },
                category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult UpdateCategory(int id, Category updatedCategory)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = updatedCategory.CategoryName;
            category.RaceID = updatedCategory.RaceID;

            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok("Category deleted successfully.");
        }
    }
}