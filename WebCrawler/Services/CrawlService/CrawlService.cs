
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WebCrawler.Services.HTTPRequestService;
using WebCrawler.Services.LinkExtractorService;

namespace Src.Controllers
{
    public class CrawlService: ICrawlService
    {
        private readonly IHTMLProvider _htmlProvider;
        private readonly ILinkExtractor _linkExtractor;
        private HashSet<string> visitedLinks;
        private Queue<string> linksToVisit;

        public CrawlService(IHTMLProvider htmlProvider, ILinkExtractor linkExtractor)
        {
            _htmlProvider = htmlProvider;
            _linkExtractor = linkExtractor;
            visitedLinks = new HashSet<string>();
            linksToVisit = new Queue<string>();
        }
        
        public List<WebPage> CrawlWebPage(string startUrl)
        {
            var results = new List<WebPage>();
            linksToVisit.Enqueue(startUrl);
            while (linksToVisit.Count > 0)
            {
                var result = new WebPage(linksToVisit.Dequeue()){ChildPages = new List<string>()} ;
                if (visitedLinks.TryGetValue(result.Url, out string visited))
                    continue;
                
                visitedLinks.Add(result.Url);

                var childLinks = _linkExtractor.ExtractLinksFromHTML(_htmlProvider.GetHTMLInWebPage(result.Url))
                    .Where(x => x.StartsWith(startUrl) || x.StartsWith("/"))
                    .Distinct();

                foreach (var childLink in childLinks)
                {
                    if(childLink.StartsWith("/"))
                        linksToVisit.Enqueue($"{startUrl}"+childLink);
                    result.ChildPages.Add(childLink);
                }

                results.Add(result);
            }

            return results;
        }
    }
}