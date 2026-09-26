using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener_API.Services.Interfaces
{
    public interface IUrlService
    {
        public Task<IActionResult> ShortenUrlAsync(string url);
        public Task<IActionResult> RedirectToOriginalUrlAsync(string shortCode);
    }
}
