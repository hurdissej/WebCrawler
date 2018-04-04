using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Src.Controllers
{
    public class CrawlController : Controller
    {
        private readonly ICrawlService _crawlService;

        public CrawlController(ICrawlService crawlService)
        {
            _crawlService = crawlService;
        }

        [HttpGet]
        [Route("/api/CrawlWebpage")]
        public WebPageDTO CrawlWebpage(string webPage)
        {
            return new WebPageDTO(){WebPages = _crawlService.CrawlWebPage(webPage)};
        }
    }

    public class WebPageDTO
    {
        public List<WebPage> WebPages { get; set; }
    }

}
