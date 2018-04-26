using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Src.Controllers;
using WebCrawler.Services.HTTPRequestService;
using WebCrawler.Services.LinkExtractorService;
using Xunit;

namespace Tests
{
    public class CrawlServiceTests
    {
        private readonly CrawlService target;
        private readonly Mock<IHTMLProvider> HtmlProvider;
        private readonly Mock<ILinkExtractor> LinkExtractor;

        public CrawlServiceTests()
        {
            HtmlProvider = new Mock<IHTMLProvider>();
            LinkExtractor = new Mock<ILinkExtractor>();
            
            target = new CrawlService(HtmlProvider.Object, LinkExtractor.Object);
        }

        [Fact]
        public void CrawlWebPage_ValidWebPagePassedWithNoLinksUnderneath_ReturnsSingleWebPage()
        {
            HtmlProvider.Setup(x => x.GetHTMLInWebPage(It.IsAny<string>())).Returns(Task.FromResult("someText"));
            LinkExtractor.Setup(x => x.ExtractLinksFromHTML(It.IsAny<string>())).Returns(new List<string>());

            var result = target.CrawlWebPage("www.google.co.uk", Int32.MaxValue).Result.First();
            
            Assert.Equal("www.google.co.uk", result.Url);
            Assert.Equal(0, result.ChildPages.Count());
        }
        
        [Fact]
        public void CrawlWebPage_ValidWebPagePassedWithLinksUnderneath_AddsChildLinksToParentPage()
        {
            HtmlProvider.Setup(x => x.GetHTMLInWebPage(It.IsAny<string>())).Returns(Task.FromResult("someText"));
            LinkExtractor.Setup(x => x.ExtractLinksFromHTML(It.IsAny<string>())).Returns(new List<string>{"www.google.co.uk/images", "www.google.co.uk/maps"});

            var result = target.CrawlWebPage("www.google.co.uk", Int32.MaxValue).Result.First(x => x.Url == "www.google.co.uk");
            
            Assert.Equal(2, result.ChildPages.Count());
        }
        
        [Fact]
        public async void CrawlWebPage_ValidWebPagePassedWithLinksUnderneath_CrawlsChildLinksAlso()
        {
            HtmlProvider.Setup(x => x.GetHTMLInWebPage(It.IsAny<string>())).Returns(Task.FromResult("someText"));
            LinkExtractor.Setup(x => x.ExtractLinksFromHTML(It.IsAny<string>())).Returns(new List<string>{"www.google.co.uk/images", "www.google.co.uk/maps"});

            var result = await target.CrawlWebPage("www.google.co.uk", Int32.MaxValue);
            
            Assert.Equal(3, result.Count());
        }
        
        [Fact]
        public async void CrawlWebPage_LinksReturnedNotFromStartWebPage_DoesNotAddAsChildLinks()
        {
            HtmlProvider.Setup(x => x.GetHTMLInWebPage(It.IsAny<string>())).Returns(Task.FromResult("someText"));
            LinkExtractor.Setup(x => x.ExtractLinksFromHTML(It.IsAny<string>())).Returns(new List<string>{"www.google.co.uk/images", "www.facebook.com"});

            var result = await target.CrawlWebPage("www.google.co.uk", Int32.MaxValue);
            
            Assert.Equal(2, result.Count());
        }
        
        
        
        
    }
}