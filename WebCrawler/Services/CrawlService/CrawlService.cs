
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
        private HashSet<string> linksToVisit;

        public CrawlService(IHTMLProvider htmlProvider, ILinkExtractor linkExtractor)
        {
            _htmlProvider = htmlProvider;
            _linkExtractor = linkExtractor;
            linksToVisit = new HashSet<string>();
        }
        
        public List<WebPage> CrawlWebPage(string startUrl, int limit)
        {
            var results = new List<WebPage>();
            linksToVisit.Add(startUrl);
            var numberOfCrawls = 0;
            while (linksToVisit.Count > 0 && numberOfCrawls < limit)
            {
                numberOfCrawls++;
                var link = linksToVisit.First();
                linksToVisit.Remove(link);
                var html = _htmlProvider.GetHTMLInWebPage(link);
                var childLinks = _linkExtractor.ExtractLinksFromHTML(html)
                    .Where(x => x.StartsWith(startUrl) || x.StartsWith("/"))
                    .Distinct();

                var result = new WebPage(link);
                foreach (var childLink in childLinks)
                {
                    if (childLink.StartsWith("/"))
                    {
                        var appendedLink = $"{startUrl}" + childLink;
                        if (appendedLink == link)
                            continue;
                        if (!results.Select(x => x.Url).Any(x => x == appendedLink)  && !linksToVisit.TryGetValue(appendedLink, out string toVisit))
                            linksToVisit.Add(appendedLink);
                        result.ChildPages.Add(appendedLink);
                    }
                    else
                    {
                        if (childLink == link)
                            continue;
                        if (!results.Select(x => x.Url).Any(x => x == childLink) && !linksToVisit.TryGetValue(childLink, out string toVisit))
                            linksToVisit.Add(childLink);
                        result.ChildPages.Add(childLink);
                    }
                }

                results.Add(result);
            }

            return results;
        }
    }
}