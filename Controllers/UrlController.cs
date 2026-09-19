using Microsoft.AspNetCore.Mvc;
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

    }
}
