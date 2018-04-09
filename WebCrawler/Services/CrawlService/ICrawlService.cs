using System.Collections.Generic;
using Src.Controllers;

public interface ICrawlService
{
    List<WebPage> CrawlWebPage(string startUrl, int limit);
}