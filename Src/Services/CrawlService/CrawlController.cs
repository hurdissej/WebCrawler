using Microsoft.AspNetCore.Mvc;

namespace Src.Controllers
{
    public class CrawlController : Controller
    {
        [HttpGet]
        [Route("/api/CrawlWeb")]
        public string CrawlWeb()
        {
            return "Abc";
        }
    }
}  