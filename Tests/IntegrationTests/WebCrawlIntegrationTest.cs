using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Src.Controllers;
using WebCrawler;
using Xunit;

namespace Tests.IntegrationTests
{
    public class WebCrawlIntegrationTest
    {
        private readonly HttpClient _client;
        public WebCrawlIntegrationTest()
        {
            // Arrange
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Production"));
            _client = server.CreateClient();
        }

        [Fact]
        public async void CrawlWeb_ValidURLPassedIn_WebPageWithLinksReturned()
        {
            // Act
            var response = await _client.GetAsync("/api/CrawlWebPage?webPage=https://monzo.com");
 
            var result = await response.Content.ReadAsStringAsync();
            var unserialisedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<WebPageDTO>(result);

            var a = unserialisedResponse;

        }
    }
}