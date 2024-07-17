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
        public async Task<IActionResult> FetchAsync()
        {
            

            HttpClient client = _httpClientFactory.CreateClient();

            HttpResponseMessage response = await client
                .GetAsync("https://api.first.org/data/v1/countries");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
       
                var jsonResult = JsonSerializer.Deserialize<dynamic>(result);

                return Ok(jsonResult);
            }

            return BadRequest(new { error = "Some error occurred" });
        }
    }
}
