using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enum;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapper.Util;

namespace GW2TPApiWrapperTest.Test
{
    [TestClass]
    public class ApiResponseConverterTest
    {
        #region valid api responses (long)
        private static readonly string _validSingleItemResponse = @"
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

        private static readonly string _validMultipleItemResponse = @"
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

        [TestMethod]
        public void ValidSignleItemResponseShouldBeConvertable()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(_validSingleItemResponse);
            MemoryStream stream = new MemoryStream(byteArray);

            Item item = ApiResponseConverter.DeserializeStream<Item>(stream);

            Assert.IsNotNull(item);
            Assert.AreEqual("Eternity", item.Name);
            Assert.AreEqual(30689, item.Id);
            Assert.AreEqual("Legendary", item.Rarity);
            Assert.AreEqual("Weapon", item.Type);
            Assert.AreEqual("Greatsword", item.Details.Type);
        }

        [TestMethod]
        public void ValidMultipleResponseShouldBeConvertable()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(_validMultipleItemResponse);
            MemoryStream stream = new MemoryStream(byteArray);

            Item[] items = ApiResponseConverter.DeserializeStream<Item[]>(stream);

            Assert.AreEqual(2, items.Count());
            Assert.AreEqual(30689, items[0].Id);
            Assert.AreEqual(30703, items[1].Id);
        }
    }
}
