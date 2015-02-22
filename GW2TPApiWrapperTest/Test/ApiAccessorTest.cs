using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Schema;
using GW2TPApiWrapper.Wrapper;
using Newtonsoft.Json.Linq;

namespace GW2TPApiWrapperTest
{
    /// Internet connection needed to perform these tests.
    /// Schemas are generated using http://jsonschema.net.
    [TestClass]
    public class ApiAccessorTest
    {
        private ApiAccessor _apiAccessor = new ApiAccessor();
        private readonly int _validItemId = 19684;

        [TestMethod]
        public void ItemIdsShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(@"
                {
                    'type': 'array',
                    'items': {
                        'type': 'integer'
                    }
                }");            
            JArray itemIds = JArray.Parse(_apiAccessor.ItemIds());
            Assert.IsTrue(itemIds.IsValid(schema));
        }

        [TestMethod]
        public void SingleItemDetailsShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(@"
                {
                  'type': 'object',
                  'properties': {
                    'name': {
                      'id': 'name',
                      'type': 'string',
	                  'required' : 'true'
                    },
                    'description': {
                      'id': 'description',
                      'type': 'string',
	                  'required' : 'true'
                    },
                    'type': {
                      'id': 'type',
                      'type': 'string',
	                  'required' : 'true'
                    },
                    'level': {
                      'id': 'level',
                      'type': 'integer',
	                  'required' : 'true'
                    },
                    'rarity': {
                      'id': 'rarity',
                      'type': 'string',
	                  'required' : 'true'
                    },
                    'vendor_value': {
                      'id': 'vendor_value',
                      'type': 'integer',
	                  'required' : 'true'
                    },
                    'id': {
                      'id': 'id',
                      'type': 'integer',
	                  'required' : 'true'
                    },
                    'icon': {
                      'id': 'icon',
                      'type': 'string',
	                  'required' : 'true'
                    }
                  },
                  'additionalProperties': true
                }");
            JObject itemDetails = JObject.Parse(_apiAccessor.ItemDetails(_validItemId));
            Assert.IsTrue(itemDetails.IsValid(schema));            
        }

        [TestMethod]
        public void ItemPriceShouldBeValidJson()
        {
            JsonSchema schema = JsonSchema.Parse(@"
                {
                    'type' : 'object',
                    'properties': {
                        'id': {
                            'id' : 'id',
                            'type' : 'integer',
                            'required' : 'true'
                        },
                        'buys': {
                            'id': 'buys',
                            'type': 'object',
                            'properties': {
                                'quantity': {
                                    'id': 'quantity',
                                    'type': 'integer'
                                },
                                'unit_price': {
                                    'id': 'unit_price',
                                    'type': 'integer'                                    
                                }
                            },
                            'required' : 'true'
                        },
                        'sells': {
                            'id': 'sells',
                            'type': 'object',
                            'properties': {
                                'quantity': {
                                    'id': 'quantity',
                                    'type': 'integer'
                                },
                                'unit_price': {
                                    'id': 'unit_price',
                                    'type': 'integer'                                    
                                }
                            },
                            'required' : 'true'
                        }
                    },
                }");

            JObject itemPrice = JObject.Parse(_apiAccessor.ItemPrice(_validItemId));
            Assert.IsTrue(itemPrice.IsValid(schema));
        }

        [TestMethod]
        public void ListingsShouldBeValidJson()
        {
            #region schema definition
            JsonSchema schema = JsonSchema.Parse(@"
                {
                  'type': 'object',
                  'properties': {
                    'id': {
                      'id': 'id',
                      'type': 'integer',
	                  'required' : 'true'
                    },
                    'buys': {
                      'id': 'buys',
                      'type': 'array',
                      'items': {
                        'id': '604',
                        'type': 'object',
                        'properties': {
                          'listings': {
                            'id': 'listings',
                            'type': 'integer',
			                'required' : 'true'
                          },
                          'unit_price': {
                            'id': 'unit_price',
                            'type': 'integer',
			                'required' : 'true'
                          },
                          'quantity': {
                            'id': 'quantity',
                            'type': 'integer',
			                'required' : 'true'
                          }
                        },
                        'additionalProperties': false,
		                'required' : 'true'
                      }
                    },
                    'sells': {
                      'id': 'sells',
                      'type': 'array',
                      'items': {
                        'id': '21',
                        'type': 'object',
                        'properties': {
                          'listings': {
                            'id': 'listings',
                            'type': 'integer',
			                'required' : 'true'
                          },
                          'unit_price': {
                            'id': 'unit_price',
                            'type': 'integer',
			                'required' : 'true'
                          },
                          'quantity': {
                            'id': 'quantity',
                            'type': 'integer',
			                'required' : 'true'
                          }
                        },
                        'additionalProperties': false,
		                'required' : 'true'
                      }
                    }
                  },
                  'additionalProperties': false            
                }");
            #endregion

            JObject itemListings = JObject.Parse(_apiAccessor.Listings(_validItemId));
            Assert.IsTrue(itemListings.IsValid(schema));
        }

        [TestMethod]
        public void IdNotFoundResponseShouldBeRecognized()
        {
            String validIdNotFoundResponse = @"{""text"":""no such id""}";
            Assert.IsTrue(_apiAccessor.IsIdNotFoundResponse(validIdNotFoundResponse));

            String invalidIdNotFoundResponse = @"
                {
                    'type' : 'object',
                    'properties': {
                        'id': {
                            'id' : 'id',
                            'type' : 'integer',
                            'required' : 'true'
                        }
                    }
                }";
            Assert.IsFalse(_apiAccessor.IsIdNotFoundResponse(invalidIdNotFoundResponse));
        }
    }
}
