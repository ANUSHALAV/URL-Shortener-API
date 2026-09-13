using Microsoft.AspNetCore.Mvc;

namespace URL_Shortener_API.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
