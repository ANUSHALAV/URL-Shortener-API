using Microsoft.AspNetCore.Mvc;
using URL_Shortener_API.Services.Interfaces;

namespace URL_Shortener_API.Services
{
    public class UrlService : IUrlService
    {
        public UrlService() { 
            
        }

        public async Task<IActionResult> ShortenUrlAsync(string url)
        {
            var shortUrl = "http://short.url/abc123"; // Replace with actual logic
            return new OkObjectResult(new { ShortUrl = shortUrl });
        }
    }
}
