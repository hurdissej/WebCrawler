using System.Collections.Generic;
using System.Threading.Tasks;
using Src.Controllers;

public interface ICrawlService
{
    Task<IEnumerable<WebPage>> CrawlWebPage(string startUrl, int limit);
}