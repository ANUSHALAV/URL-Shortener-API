using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener_API.Services.Interfaces;

namespace URL_Shortener_API.Controllers
{
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        [Route("shorten")]
        public async Task<IActionResult> Shorten(string URL)
        {
            if (string.IsNullOrEmpty(URL))
            {
                return BadRequest("URL parameter is required.");
            }
            else if (!Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                return BadRequest("Invalid URL format.");
            }
            else
            {
                var shortUrl = await _urlService.ShortenUrlAsync(URL);
            }

            return Ok();
        }


        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortCode)
        {
            var shortUrl = await _urlService.RedirectToOriginalUrlAsync(shortCode);

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
