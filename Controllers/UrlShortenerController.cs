using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener_API.Services.Interfaces;

namespace URL_Shortener_API.Controllers
{
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlShortenerController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        [Route("CreateShortUrl")]
        public async Task<IActionResult> CreateShortUrlAsync(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest("URL parameter is required.");
            }
            else if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return BadRequest("Invalid URL format.");
            }
            else
            {
                var shortUrl = await _urlService.CreateShortUrlAsync(url);
            }

            return Ok();
        }


        [HttpGet]
        [Route("CallShortUrl")]
        public async Task<IActionResult> CallShortUrlAsync(string shortCode)
        {
            var shortUrl = await _urlService.CallShortUrlAsync(shortCode);

            if (shortUrl == null)
            {
                return NotFound(new
                {
                    Message = "Short URL not found."
                });
            }

            // Redirect to original URL
            return Ok(shortUrl);
        }

    }
}
