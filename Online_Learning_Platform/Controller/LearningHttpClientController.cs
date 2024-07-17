using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Online_Learning_Platform.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningHttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public LearningHttpClientController(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        [HttpGet("/fetch")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var client = _clientFactory.CreateClient();

                // Send a GET request to the API endpoint
                HttpResponseMessage response = await client
                    .GetAsync("https://api.first.org/data/v1/countries");

                // Check if the response is successful (status code 200-299)
                if (response.IsSuccessStatusCode)
                {
                    // Read the content of the response
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Return the fetched data
                    return Ok(responseBody);
                }
                else
                {
                    // Return appropriate status code and message
                    return StatusCode((int)response.StatusCode, $"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Return an error response with the exception message
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
