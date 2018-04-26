using System;
using WebCrawler.Services.HTTPRequestService;
using Xunit;

namespace Tests
{
    public class HtmlProviderTests
    {
        private readonly HTMLProvider target;
        public HtmlProviderTests()
        {
            target = new HTMLProvider();
        }
        
        [Fact]
        public async void GetHTMLInPage_InValidURLProvided_EmptyStringReturned()
        {
            var invalidUrl = "notAUrl";
            var result = await target.GetHTMLInWebPage(invalidUrl);
            Assert.Equal(result, String.Empty);
        }

        [Fact]
        public async void GetHTMLInPage_ValidURLProvided_HTMLReturned()
        {
            var validUrl = "https://google.co.uk";
            var result = await target.GetHTMLInWebPage(validUrl);
            Assert.Equal(result[0], '<');
        }
    }
}
