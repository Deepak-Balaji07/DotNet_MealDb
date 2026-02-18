using MealDb.Models;
using System.Text.Json;

namespace MealDb.Services
{
    public class MealService
    {
        private readonly HttpClient _httpClient;

        public MealService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<T?> GetFromMealDb<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // 🔍 Search by name
        public Task<MealResponse?> SearchByName(string name) =>
            GetFromMealDb<MealResponse>(
                $"https://www.themealdb.com/api/json/v1/1/search.php?s={name}");

        // 🔠 Search by first letter
        public Task<MealResponse?> SearchByFirstLetter(string letter) =>
            GetFromMealDb<MealResponse>(
                $"https://www.themealdb.com/api/json/v1/1/search.php?f={letter}");

        // Meal details by ID
        public Task<MealResponse?> GetMealById(string id) =>
            GetFromMealDb<MealResponse>(
                $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}");


        // 📂 Categories
        public Task<object?> GetCategories() =>
            GetFromMealDb<object>(
                "https://www.themealdb.com/api/json/v1/1/categories.php");

        // 🌍 Areas
        public Task<object?> GetAreas() =>
            GetFromMealDb<object>(
                "https://www.themealdb.com/api/json/v1/1/list.php?a=list");

        // 🧾 Filter by category
        public Task<object?> FilterByCategory(string category) =>
            GetFromMealDb<object>(
                $"https://www.themealdb.com/api/json/v1/1/filter.php?c={category}");

        // 🗺️ Filter by area
        public Task<object?> FilterByArea(string area) =>
            GetFromMealDb<object>(
                $"https://www.themealdb.com/api/json/v1/1/filter.php?a={area}");
    }
}
