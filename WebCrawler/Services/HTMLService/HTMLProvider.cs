using System;
using System.IO;
using System.Net;

namespace WebCrawler.Services.HTTPRequestService
{
    public class HTMLProvider : IHTMLProvider
    {
        public HTMLResponse GetHTMLInWebPage(string url)
        {
            if (!IsURlValid(url))
                return new HTMLResponse();

            return TryGetHTMLFromURL(url);
        }

        private HTMLResponse TryGetHTMLFromURL(string url)
        {
            var retries = 0;
            while (retries < 10)
            {
                retries++;
                try
                {
                    HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
                    HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                    var stream = new StreamReader(response.GetResponseStream());
                    var html =  stream.ReadToEnd();
                    return new HTMLResponse()
                    {
                        URL = url,
                        HTML = html
                    };
                }
                catch (WebException e)
                {
                    var response = (HttpWebResponse) e.Response;
                    if (response.StatusCode == HttpStatusCode.Moved)
                        url = response.Headers["Location"];
                }   
            }

            return new HTMLResponse();
        }


        private bool IsURlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}