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
        public WebPageDTO CrawlWebpage(string webPage, int limit)
        {
            if(limit == 0)
                return new WebPageDTO{WebPages = _crawlService.CrawlWebPage(webPage, int.MaxValue)};
            return new WebPageDTO{WebPages = _crawlService.CrawlWebPage(webPage, limit)};
        }
    }

    public class WebPageDTO
    {
        public List<WebPage> WebPages { get; set; }
    }

}
