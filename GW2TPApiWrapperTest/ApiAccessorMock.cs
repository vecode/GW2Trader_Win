using GW2TPApiWrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TPApiWrapperTest
{
    class ApiAccessorMock : IApiAccessor
    {
        #region valid results
        private int _validTestId = 30689;

        private String _validItemDetailsAsJson = @"
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

        private String _validItemPriceAsJson = @"
            {
                'id':30689,
                'buys':{
                    'quantity':29728,
                    'unit_price':35560206
                },
                'sells':{
                    'quantity':19,
                    'unit_price':41980000
                }
            }";

        private String _validItemListingsAsJson = @"
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
        #endregion

        public string ItemIds()
        {
            // valid id list
            return @"[1,2,11]";
        }

        public string ItemDetails(int id)
        {
            return id == _validTestId ? _validItemDetailsAsJson : null;
        }

        public string ItemPrice(int id)
        {
            return id == _validTestId ? _validItemPriceAsJson : null;
        }

        public string Listings(int id)
        {
            return id == _validTestId ? _validItemListingsAsJson : null;
        }
    }
}
