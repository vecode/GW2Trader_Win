using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using GW2Trader.ApiWrapper.Wrapper;
using GW2TPApiWrapperTest.Util;

namespace GW2TPApiWrapperTest.Mock
{
    public class ApiAccessorMock : IApiAccessor
    {
        private IApiAccessor _apiAccessor = new ApiAccessor(new TestWebClientProvider());

        #region test data
        private const int ValidTestId = 30689;

        private const string ValidItemAsJson = @"
            {
                'name':'Eternity',
                'type':'Weapon',
                'level':80,
                'rarity':'Legendary',
                'vendor_value':100000,
                'default_skin':4678,
                'game_types':[
                    'Activity',
                    'Dungeon',
                    'Pve',
                    'Wvw'
                ],
                'flags':[
                    'HideSuffix',
                    'NoSalvage',
                    'NoSell',
                    'AccountBindOnUse'
                ],
                'restrictions':[
                ],
                'id':30689,
                'icon':'https://render.guildwars2.com/file/A30DA1A1EF05BD080C95AE2EF0067BADCDD0D89D/456014.png',
                'details':{
                    'type':'Greatsword',
                    'damage_type':'Physical',
                    'min_power':1045,
                    'max_power':1155,
                    'defense':0,
                    'infusion_slots':[
                        {
                            'flags':[
                                'Offense'
                            ]
                        },
                        {
                            'flags':[
                                'Offense'
                            ]
                        }
                    ],
                    'suffix_item_id':24599,
                    'secondary_suffix_item_id':''
                }
            }";

        private const string ValidItemArrayAsJson = @"
            [
                {
                'name':'MONSTER ONLY Moa Unarmed Pet',
                'type':'Weapon',
                'level':0,
                'rarity':'Fine',
                'vendor_value':0,
                'default_skin':3265,
                'game_types':[
	                'Activity',
	                'Dungeon',
	                'Pve',
	                'Wvw'
                ],
                'flags':[
	                'NoSell',
	                'SoulbindOnAcquire',
	                'SoulBindOnUse'
                ],
                'restrictions':[

                ],
                'id':1,
                'icon':'https://render.guildwars2.com/file/4AECE5EA59CA057F4C53E1EDFE95E0E3E61DE37F/60980.png',
                'details':{
	                'type':'Staff',
	                'damage_type':'Physical',
	                'min_power':146,
	                'max_power':165,
	                'defense':0,
	                'infusion_slots':[

	                ],
	                'infix_upgrade':{
	                'attributes':[

	                ]
	                },
	                'secondary_suffix_item_id':''
                }
                },
                {
                'name':'Assassin Pill',
                'description':'Take this pill to participate in the next round of Assassin',
                'type':'Consumable',
                'level':0,
                'rarity':'Basic',
                'vendor_value':0,
                'game_types':[
	                'Dungeon',
	                'Pve',
	                'Wvw'
                ],
                'flags':[
	                'NoSell',
	                'SoulbindOnAcquire',
	                'SoulBindOnUse'
                ],
                'restrictions':[

                ],
                'id':2,
                'icon':'https://render.guildwars2.com/file/ED903431B97968C79AEC7FB21535FC015DBB0BBA/60981.png',
                'details':{
	                'type':'Food'
                }
            }]";

        private const string ValidItemListingsAsJson = @"
            {
                'id':30689,
                'buys':[
                    {
                        'listings':13,
                        'unit_price':4,
                        'quantity':49
                    },
                    {
                        'listings':37,
                        'unit_price':3,
                        'quantity':293
                    }
                ],
                'sells':[
                    {
                        'listings':1,
                        'unit_price':41980000,
                        'quantity':1
                    },
                    {
                        'listings':1,
                        'unit_price':41990000,
                        'quantity':1
                    }
                ]
            }";

        private const string ValidItemListingsArrayAsJson = @"
            [
                {
                    'id':24,
                    'buys':[
                        {
                            'listings':6,
                            'unit_price':101,
                            'quantity':1410
                        },
                        {
                            'listings':10,
                            'unit_price':99,
                            'quantity':2499
                        }
                    ],
                    'sells':[
                        {
                            'listings':1,
                            'unit_price':179,
                            'quantity':43
                        },
                        {
                            'listings':1,
                            'unit_price':180,
                            'quantity':49
                        }
                    ]
                },
                {
                    'id':68,
                    'buys':[
                        {
                            'listings':2,
                            'unit_price':116,
                            'quantity':498
                        },
                        {
                            'listings':1,
                            'unit_price':115,
                            'quantity':32
                        }
                    ],
                    'sells':[
                        {
                            'listings':2,
                            'unit_price':397,
                            'quantity':2
                        },
                        {
                            'listings':1,
                            'unit_price':398,
                            'quantity':1
                        }
                    ]
                }
            ]";

        private const string ValidMultiplePricesAsJson = @"
            [
                {
                    'id':24,
                    'buys':{
                        'quantity':18891,
                        'unit_price':93
                    },
                    'sells':{
                        'quantity':20835,
                        'unit_price':175
                    }
                },
                {
                    'id':68,
                    'buys':{
                        'quantity':2219,
                        'unit_price':117
                    },
                    'sells':{
                        'quantity':311,
                        'unit_price':390
                    }
                },
                {
                    'id':69,
                    'buys':{
                        'quantity':2865,
                        'unit_price':182
                    },
                    'sells':{
                        'quantity':353,
                        'unit_price':381
                    }
                }
            ]";

        #endregion

        public Stream ItemIds()
        {
            // valid id list
            return StringToStream(@"[1,2,11]");
        }

        public Stream ItemDetails(int id)
        {
            return ValidTestId == id ? StringToStream(ValidItemAsJson) : null;
        }

        public Stream Listings(int id)
        {
            return id == ValidTestId ? StringToStream(ValidItemListingsAsJson) : null;
        }

        public Stream ItemDetails(int[] ids)
        {
            if (ids[0] == 1 && ids[1] == 2)
                return StringToStream(ValidItemArrayAsJson);
            return null;
        }

        private Stream StringToStream(String str)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(str);
            return new MemoryStream(byteArray);
        }

        public Stream Listings(int[] ids)
        {
            if (ids[0] == 24 && ids[1] == 68)
                return StringToStream(ValidItemListingsArrayAsJson);
            return null;
        }

        public Stream Prices(int[] ids)
        {
            return StringToStream(ValidMultiplePricesAsJson);
        }
    }
}
