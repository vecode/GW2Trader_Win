using System;
using Newtonsoft.Json.Schema;
using GW2TPApiWrapper.Wrapper;
using Newtonsoft.Json.Linq;
using System.IO;
using Xunit;

namespace GW2TPApiWrapperTest.Test
{
    /// Internet connection needed to perform these tests    
    public class ApiAccessorTest
    {
        private readonly IApiAccessor _apiAccessor = new ApiAccessor();
        private const int ValidItemId = 19684;

        [Fact]
        public void IdListShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.IdListSchema);
            JArray itemIds = JArray.Parse(StreamToString(_apiAccessor.ItemIds()));
            Assert.True(itemIds.IsValid(schema));
        }

        [Fact]
        public void SingleItemShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.ItemSchema);
            JObject itemDetails = JObject.Parse(StreamToString(_apiAccessor.ItemDetails(ValidItemId)));
            Assert.True(itemDetails.IsValid(schema));
        }

        [Fact]
        public void MultipleItemsShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.ItemArraySchema);
            string jsonResult = StreamToString(_apiAccessor.ItemDetails(new int[] { 1, 2 }));
            JArray itemArray = JArray.Parse(jsonResult);
            Assert.True(itemArray.IsValid(schema));
        }

        [Fact]
        public void ListingsShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.ItemListingSchema);
            JObject itemListings = JObject.Parse(StreamToString(_apiAccessor.Listings(ValidItemId)));

            Assert.True(itemListings.IsValid(schema));
        }

        private string StreamToString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            stream.Close();
            return text;
        }
    }
}
