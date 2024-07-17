using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningHttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LearningHttpClientController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("/fetch")]
        public async Task<List<string>> FetchAsync()
        {
            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client
                .GetAsync("https://api.first.org/data/v1/countries");

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize JSON response
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                // List to store country names
                List<string> countryNames = new List<string>();

                // Navigate through the JSON structure
                if (jsonDocument.RootElement.TryGetProperty("data", out var dataElement) && dataElement.ValueKind == JsonValueKind.Object)
                {
                    foreach (var countryProperty in dataElement.EnumerateObject())
                    {
                        var countryName = countryProperty.Value.GetProperty("country").GetString();
                        countryNames.Add(countryName);
                    }
                }

                return countryNames;
            }
            else
            {
                // Handle unsuccessful response
                return null;
            }

        }
    }
}
