using System.Collections.Generic;

namespace Src.Controllers
{
    public class WebPage
    {
        public WebPage(string url)
        {
            Url = url;
            ChildPages = new List<string>();
        }
        public string Url { get; private set; }
        public List<string> ChildPages { get; set; }
    }
}