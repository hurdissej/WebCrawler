using System;
using System.Net;

namespace WebCrawler.Services.HTTPRequestService
{
    public class HTMLProvider: IHTMLProvider
    {
        public string GetHTMLInWebPage(string url)
        {
            if (!IsURlValid(url))
                return string.Empty;

            using (WebClient client = new WebClient())
            {
                try
                {
                    var redirectedLocation = GetRedirectLocation(url);
                    string htmlCode = client.DownloadString(redirectedLocation);
                    return htmlCode;
                }
                catch (WebException e)
                {
                    return string.Empty;
                }
            }
        }

        private string GetRedirectLocation(string originalUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(originalUrl);
            request.AllowAutoRedirect = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.Headers["Location"];
        }

        private bool IsURlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}