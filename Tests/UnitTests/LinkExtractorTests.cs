using System;
using System.Collections.Generic;
using System.Linq;
using WebCrawler.Services.LinkExtractorService;
using Xunit;

namespace Tests
{
    public class LinkExtractorTests
    {
        private readonly LinkExtractor target;
        
        public LinkExtractorTests()
        {
            target = new LinkExtractor();    
        }
        
        [Fact]
        public void ExtractLinksFromHTML_StringWithNoLinksPresent_ReturnsEmptyList()
        {
            var stringWithNoLinks = "<html> <h1> Some heading </h1> </html>";
            var result = target.ExtractLinksFromHTML(stringWithNoLinks);
            
            Assert.Equal(0, result.Count());
        }
        
        [Fact]
        public void ExtractLinksFromHTML_StringWithLinkPresent_ReturnsLinkAsList()
        {
            var stringWithNoLinks = "<html> <h1> Some heading </h1> <a href=\"google.co.uk\"</a> </html>";
            var result = target.ExtractLinksFromHTML(stringWithNoLinks);
            Assert.Equal("google.co.uk", result.First());
            Assert.Equal(1, result.Count());
        }
        
        [Fact]
        public void ExtractLinksFromHTML_StringWithLinksInImagesPresent_ReturnsAllAsSeparateItems()
        {
            var stringWithNoLinks = "<html> <h1> Some heading </h1> " +
                                    "<a href=\"google.co.uk\"></a> " + "<img href=\"LinkToMyImage.co.uk\"" +
                                    "</html>";
            var result = target.ExtractLinksFromHTML(stringWithNoLinks);
            Assert.Equal(1, result.Count());
        }
        
        [Fact]
        public void ExtractLinksFromHTML_SameLinkPresentTwice_OnlyReturnsItemOnce()
        {
            var stringWithNoLinks = "<html> <h1> Some heading </h1> <a href=\"google.co.uk\"</a> </html>"
                                    + "<html> <h1> Some heading </h1> <a href=\"google.co.uk\" </html>";
            var result = target.ExtractLinksFromHTML(stringWithNoLinks);
            
            Assert.Equal(1, result.Count());
        }
    }
}