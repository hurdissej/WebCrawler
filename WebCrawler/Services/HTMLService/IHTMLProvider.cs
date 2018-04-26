using System.Threading.Tasks;

namespace WebCrawler.Services.HTTPRequestService
{
    public interface IHTMLProvider
    {
        Task<string> GetHTMLInWebPage(string url);
    }
}