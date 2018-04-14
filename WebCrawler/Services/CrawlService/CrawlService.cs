
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebCrawler.Services.HTTPRequestService;
using WebCrawler.Services.LinkExtractorService;

namespace Src.Controllers
{
    public class CrawlService: ICrawlService
    {
        private readonly IHTMLProvider _htmlProvider;
        private readonly ILinkExtractor _linkExtractor;
        private ConcurrentQueue<string> linksToVisit;

        public CrawlService(IHTMLProvider htmlProvider, ILinkExtractor linkExtractor)
        {
            _htmlProvider = htmlProvider;
            _linkExtractor = linkExtractor;
            linksToVisit = new ConcurrentQueue<string>();
        }
        
        public List<WebPage> CrawlWebPage(string startUrl, int limit)
        {
            var results = new ConcurrentBag<WebPage>();
            linksToVisit.Enqueue(startUrl);
            var numberOfCrawls = 0;
            while (linksToVisit.Count > 0 && numberOfCrawls < limit)
            {
                numberOfCrawls++;
                linksToVisit.TryDequeue(out string link);
                var html = _htmlProvider.GetHTMLInWebPage(link);
                var childLinks = _linkExtractor.ExtractLinksFromHTML(html)
                    .Where(x => x.StartsWith(startUrl) || x.StartsWith("/"));

                var result = new WebPage(link);
                Parallel.ForEach(childLinks, (childLink) =>
                {
                    if (childLink.StartsWith("/"))
                    {
                        var appendedLink = $"{startUrl}" + childLink;
                        if (appendedLink == link)
                            return;
                        if (results.Select(x => x.Url).All(x => x != appendedLink) && !linksToVisit.Any(x => x == appendedLink))
                            linksToVisit.Enqueue(appendedLink);
                        result.ChildPages.Add(appendedLink);
                    }
                    else
                    {
                        if (childLink == link)
                            return;
                        if (results.Select(x => x.Url).All(x => x != childLink) && !linksToVisit.Any(x => x == childLink))
                            linksToVisit.Enqueue(childLink);
                        result.ChildPages.Add(childLink);
                    }
                });

                results.Add(result);
            }

            return results.ToList();
        }
    }
}