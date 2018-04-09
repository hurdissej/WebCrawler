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
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Production"));
            _client = server.CreateClient();
        }

        [Fact]
        public async void CrawlWeb_ValidURLPassedIn_WebPageWithLinksReturned()
        {
            var unserialisedResponse = await GetWebResult("https://monzo.com", 10);
            Assert.Equal(10, unserialisedResponse.WebPages.Count);
        }

        private async Task<WebPageDTO> GetWebResult(string startUrl, int limit)
        {
            var response = await _client.GetAsync($"/api/CrawlWebPage?webPage={startUrl}&limit={limit}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<WebPageDTO>(result);   
        }
    }
}