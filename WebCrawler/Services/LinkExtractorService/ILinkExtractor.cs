using System.Collections.Generic;

namespace WebCrawler.Services.LinkExtractorService
{
    public interface ILinkExtractor
    {
        IEnumerable<string> ExtractLinksFromHTML(string Html);
    }
}