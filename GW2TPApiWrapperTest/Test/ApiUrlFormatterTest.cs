using System;
using GW2TPApiWrapper.Util;
using Xunit;

namespace GW2TPApiWrapperTest.Test
{
    public class ApiUrlFormatterTest
    {
        [Fact]
        public void UrlWithSingleIdShouldBeValid()
        {
            int id = 1;
            string url = @"http://url_to_api/items";
            string formattedUrl = ApiUrlFormatter.FormatUrl(url, id);

            Assert.Equal(@"http://url_to_api/items/1", formattedUrl);
        }

        [Fact]
        public void UrlWithMultipleIdsShouldBeValid()
        {
            int[] ids = { 1, 2, 3 };
            string url = @"http://url_to_api/items/";
            string formattedUrl = ApiUrlFormatter.FormatUrl(url, ids);

            Assert.Equal(@"http://url_to_api/items/?ids=1,2,3", formattedUrl);
        }
    }
}
