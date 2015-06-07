using System;
using System.Text;
using System.IO;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enum;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapper.Util;
using Xunit;

namespace GW2TPApiWrapperTest.Test
{
    public class ApiResponseConverterTest
    {
        #region valid api responses (long)
        private const string ValidSingleItemResponse = @"
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

        private const string ValidMultipleItemResponse = @"
            [
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
                },
                {
                    'name':'Sunrise',
                    'type':'Weapon',
                    'level':80,
                    'rarity':'Legendary',
                    'vendor_value':100000,
                    'default_skin':4679,
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
                    'id':30703,
                    'icon':'https://render.guildwars2.com/file/EFF16C4F19792627355DC294E6D7093F544921E7/456030.png',
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
                        'suffix_item_id':24562,
                        'secondary_suffix_item_id':''
                    }
                }
            ]";

        #endregion

        [Fact]
        public void ValidSignleItemResponseShouldBeConvertable()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(ValidSingleItemResponse);
            MemoryStream stream = new MemoryStream(byteArray);

            ApiItem item = ApiResponseConverter.DeserializeStream<ApiItem>(stream);

            Assert.NotNull(item);
            Assert.Equal("Eternity", item.Name);
            Assert.Equal(30689, item.Id);
            Assert.Equal("Legendary", item.Rarity);
            Assert.Equal("Weapon", item.Type);
            Assert.Equal("Greatsword", item.Details.Type);
        }

        [Fact]
        public void ValidMultipleItemResponseShouldBeConvertable()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(ValidMultipleItemResponse);
            MemoryStream stream = new MemoryStream(byteArray);

            ApiItem[] items = ApiResponseConverter.DeserializeStream<ApiItem[]>(stream);

            Assert.Equal(2, items.Count());
            Assert.Equal(30689, items[0].Id);
            Assert.Equal(30703, items[1].Id);
        }
    }
}
