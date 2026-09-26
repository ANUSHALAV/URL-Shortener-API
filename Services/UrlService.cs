using Microsoft.AspNetCore.Mvc;
using URL_Shortener_API.Data;
using URL_Shortener_API.Models;
using URL_Shortener_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace URL_Shortener_API.Services
{
    public class UrlService : IUrlService
    {
        private readonly AppDbContext _context;

        public UrlService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShortenUrlAsync(string url)
        {
            // 1. Validate URL
            if (string.IsNullOrWhiteSpace(url))
            {
                return new BadRequestObjectResult(new
                {
                    Message = "URL is required."
                });
            }

            // 2. Validate URL format
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp &&
                 uri.Scheme != Uri.UriSchemeHttps))
            {
                return new BadRequestObjectResult(new
                {
                    Message = "Please provide a valid HTTP or HTTPS URL."
                });
            }

            // 3. Generate unique short code
            string shortCode;

            do
            {
                shortCode = GenerateShortCode(6);

            } while (_context.ShortUrls.Any(x => x.ShortCode == shortCode));

            // 4. Create entity
            var shortUrl = new ShortUrl
            {
                OriginalUrl = url,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow,
                ClickCount = 0
            };

            // 5. Save to database
            _context.ShortUrls.Add(shortUrl);

            await _context.SaveChangesAsync();

            // 6. Create short URL
            var shortUrlValue = $"https://localhost:7000/{shortCode}";

            // 7. Return response
            return new OkObjectResult(new
            {
                Message = "URL shortened successfully.",
                OriginalUrl = url,
                ShortUrl = shortUrlValue,
                ShortCode = shortCode
            });
        }

        private string GenerateShortCode(int length)
        {
            const string characters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            var random = new Random();

            return new string(
                Enumerable.Range(0, length)
                    .Select(_ => characters[random.Next(characters.Length)])
                    .ToArray()
            );
        }

        public async Task<IActionResult> RedirectToOriginalUrlAsync(string shortCode)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(x => x.ShortCode == shortCode);

            if (shortUrl == null)
            {
                return new NotFoundObjectResult(new
                {
                    Message = "Short URL not found."
                });
            }

            shortUrl.ClickCount++;

            await _context.SaveChangesAsync();

            return new RedirectResult(shortUrl.OriginalUrl);
        }
    }
}