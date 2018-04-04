namespace WebCrawler.Services.HTTPRequestService
{
    public interface IHTMLProvider
    {
        string GetHTMLInWebPage(string url);
    }
}