using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebCrawler.Services.LinkExtractorService
{
    public class LinkExtractor: ILinkExtractor
    {
        public IEnumerable<string> ExtractLinksFromHTML(string Html)
        {
            var results = new List<string>();
            var anchorTags = Regex.Matches(Html.ToLower(), @"<a(.*?)</a>").Select(x => x.Value);

            foreach (var tag in anchorTags)
            {
                results.AddRange(Regex.Matches(tag, @"href=\""(.*?)\""", RegexOptions.Singleline)
                    .Select(match => match.Value.Replace("href=\"", "").Replace("\"",""))
                    .ToList());
            }

            return results.Distinct();
        }
    }
}