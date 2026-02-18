using MealDb.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealDb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealController : ControllerBase
    {
        private readonly MealService mealService;

        public MealController(MealService mealService)
        {
            this.mealService = mealService;
        }

        // 🔍 Search by name
        [HttpGet("search/{name}")]
        public async Task<IActionResult> SearchByName(string name)
            => Ok(await mealService.SearchByName(name));

        // 🔠 Search by first letter
        [HttpGet("letter/{letter}")]
        public async Task<IActionResult> SearchByLetter(string letter)
            => Ok(await mealService.SearchByFirstLetter(letter));

        // Meal details by ID
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetMealDetails(string id)
        {
            return Ok(await mealService.GetMealById(id));
        }


        // 📂 Categories
        [HttpGet("categories")]
        public async Task<IActionResult> Categories()
            => Ok(await mealService.GetCategories());

        // 🌍 Areas
        [HttpGet("areas")]
        public async Task<IActionResult> Areas()
            => Ok(await mealService.GetAreas());

        // 🧾 Filter by category
        [HttpGet("category/{category}")]
        public async Task<IActionResult> FilterByCategory(string category)
            => Ok(await mealService.FilterByCategory(category));

        // 🗺️ Filter by area
        [HttpGet("area/{area}")]
        public async Task<IActionResult> FilterByArea(string area)
            => Ok(await mealService.FilterByArea(area));
    }
}
