using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<WebPageDTO> CrawlWebpage(string webPage, int limit)
        {
            var sw = new Stopwatch();
            sw.Start();
            if(limit == 0)
                return new WebPageDTO{WebPages = await _crawlService.CrawlWebPage(webPage, int.MaxValue), TimeTaken = sw.ElapsedMilliseconds};
            return new WebPageDTO{WebPages = await _crawlService.CrawlWebPage(webPage, limit), TimeTaken = sw.ElapsedMilliseconds};
        }
    }

    public class WebPageDTO
    {
        public double TimeTaken { get; set; }
        public IEnumerable<WebPage> WebPages { get; set; }
    }

}
