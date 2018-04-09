namespace WebCrawler.Services.HTTPRequestService
{
    public interface IHTMLProvider
    {
        HTMLResponse GetHTMLInWebPage(string url);
    }
}