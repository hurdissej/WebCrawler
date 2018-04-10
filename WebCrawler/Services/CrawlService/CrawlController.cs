using System.Collections.Generic;
using System.Diagnostics;
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
            var sw = new Stopwatch();
            sw.Start();
            if(limit == 0)
                return new WebPageDTO{WebPages = _crawlService.CrawlWebPage(webPage, int.MaxValue), TimeTaken = sw.ElapsedMilliseconds};
            return new WebPageDTO{WebPages = _crawlService.CrawlWebPage(webPage, limit), TimeTaken = sw.ElapsedMilliseconds};
        }
    }

    public class WebPageDTO
    {
        public double TimeTaken { get; set; }
        public List<WebPage> WebPages { get; set; }
    }

}
