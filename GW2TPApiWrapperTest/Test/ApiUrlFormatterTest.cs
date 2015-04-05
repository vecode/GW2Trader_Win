using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2TPApiWrapper.Util;

namespace GW2TPApiWrapperTest.Test
{
    [TestClass]
    public class ApiUrlFormatterTest
    {
        [TestMethod]
        public void UrlWithSingleIdShouldBeValid()
        {
            int id = 1;
            string url = @"http://url_to_api/items";
            string formattedUrl = ApiUrlFormatter.FormatUrl(url, id);

            Assert.AreEqual(@"http://url_to_api/items/1", formattedUrl);
        }

        [TestMethod]
        public void UrlWithMultipleIdsShouldBeValid()
        {
            int[] ids = { 1, 2, 3 };
            string url = @"http://url_to_api/items/";
            string formattedUrl = ApiUrlFormatter.FormatUrl(url, ids);

            Assert.AreEqual(@"http://url_to_api/items/?ids=1,2,3", formattedUrl);
        }
    }
}
