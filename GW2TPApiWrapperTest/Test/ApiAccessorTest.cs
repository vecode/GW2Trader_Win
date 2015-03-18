using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;
using GW2TPApiWrapper.Wrapper;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GW2TPApiWrapperTest.Test
{
    // TODO redo test cases for ApiAccessorTest
    /// Internet connection needed to perform these tests.
    [TestClass]
    public class ApiAccessorTest
    {
        private readonly IApiAccessor _apiAccessor = new ApiAccessor();
        private const int ValidItemId = 19684;

        [TestMethod]
        public void IdListShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.IdListSchema);
            JArray itemIds = JArray.Parse(StreamToString(_apiAccessor.ItemIds()));
            Assert.IsTrue(itemIds.IsValid(schema));
        }

        [TestMethod]
        public void SingleItemShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.ItemSchema);
            JObject itemDetails = JObject.Parse(StreamToString(_apiAccessor.ItemDetails(ValidItemId)));
            Assert.IsTrue(itemDetails.IsValid(schema));
        }


        [TestMethod]
        public void MultipleItemsShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.ItemArraySchema);
            string jsonResult = StreamToString(_apiAccessor.ItemDetails(new int[] { 1, 2 }));
            JArray itemArray = JArray.Parse(jsonResult);
            Assert.IsTrue(itemArray.IsValid(schema));
        }

        //        [TestMethod]
        //        public void ListingsShouldBeValidJson()
        //        {
        //            JsonSchema schema = JsonSchema.Parse(JsonResultSchema.ItemListingSchema);
        //            JObject itemListings = JObject.Parse(_apiAccessor.Listings(_validItemId));

        //            Assert.IsTrue(itemListings.IsValid(schema));
        //        }

        //        [TestMethod]
        //        public void IdNotFoundResponseShouldBeRecognized()
        //        {
        //            String validIdNotFoundResponse = @"{""text"":""no such id""}";
        //            Assert.IsTrue(_apiAccessor.IsIdNotFoundResponse(validIdNotFoundResponse));

        //            String invalidIdNotFoundResponse = @"
        //                {
        //                    'type' : 'object',
        //                    'properties': {
        //                        'id': {
        //                            'id' : 'id',
        //                            'type' : 'integer',
        //                            'required' : 'true'
        //                        }
        //                    }
        //                }";
        //            Assert.IsFalse(_apiAccessor.IsIdNotFoundResponse(invalidIdNotFoundResponse));
        //        }

        private string StreamToString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            stream.Close();
            return text;
        }
    }
}
