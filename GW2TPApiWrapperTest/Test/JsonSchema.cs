using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapperTest.Test
{
    public static class JsonResultSchema
    {
        // schemas were generated using http://jsonschema.net.

        public static readonly string IdListSchema = @"
            {
                'type': 'array',
                'items': {
                    'type': 'integer'
                }
            }";

        public static readonly string ItemSchema = @"
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
            }";

        public static readonly string ItemArraySchema = @"
            {
              'type': 'array',
              'items': {
                'id': '1',
                'type': 'object',
                'properties': {
                  'name': {
                    'id': 'name',
                    'type': 'string'
                  },
                  'description': {
                    'id': 'description',
                    'type': 'string'
                  },
                  'type': {
                    'id': 'type',
                    'type': 'string'
                  },
                  'level': {
                    'id': 'level',
                    'type': 'integer'
                  },
                  'rarity': {
                    'id': 'rarity',
                    'type': 'string'
                  },
                  'vendor_value': {
                    'id': 'vendor_value',
                    'type': 'integer'
                  },
                  'game_types': {
                    'id': 'game_types',
                    'type': 'array',
                    'items': {
                      'id': '2',
                      'type': 'string'
                    }
                  },
                  'flags': {
                    'id': 'flags',
                    'type': 'array',
                    'items': {
                      'id': '2',
                      'type': 'string'
                    }
                  },
                  'restrictions': {
                    'id': 'restrictions',
                    'type': 'array',
                    'items': {}
                  },
                  'id': {
                    'id': 'id',
                    'type': 'integer'
                  },
                  'icon': {
                    'id': 'icon',
                    'type': 'string'
                  },
                  'details': {
                    'id': 'details',
                    'type': 'object',
                    'properties': {
                      'type': {
                        'id': 'type',
                        'type': 'string'
                      }
                    }
                  }
                }
              }
            }";

        public static readonly string ItemListingSchema = @"
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
            }";
    }
}
