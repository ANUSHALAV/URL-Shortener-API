using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener_API.Services.Interfaces
{
    public interface IUrlService
    {
        public Task<IActionResult> CreateShortUrlAsync(string url);
        public Task<IActionResult> CallShortUrlAsync(string shortCode);
    }
}
