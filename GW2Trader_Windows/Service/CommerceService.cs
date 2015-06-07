using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader_Windows.Data;
using GW2Trader_Windows.Model;
using Newtonsoft.Json;

namespace GW2Trader_Windows.Service
{
    public class CommerceService : ICommerceService
    {
        private readonly ITradingPostApiWrapper _tpWrapper;
        private readonly INotifyService _notifyService;

        private const string WebExceptionErrorMessage = "Not able to connect GW2 Api";
        private const string JsonExceptionErrorMessage = "Api response format changed, not able to convert data";
             

        public CommerceService(ITradingPostApiWrapper tpWrapper, INotifyService notifyService)
        {
            _tpWrapper = tpWrapper;
            _notifyService = notifyService;
        }

        public void UpdatePrices(IEnumerable<Model.GameItemModel> items)
        {
            try
            {
                var gameItemModels = items as GameItemModel[] ?? items.ToArray();
                List<ApiItemPrice> updatedPrices = _tpWrapper.Prices(gameItemModels.Select(item => item.ItemId)).ToList();
                foreach (GameItemModel item in gameItemModels)
                {
                    ApiItemPrice respectivePrice = updatedPrices.SingleOrDefault(p => p.Id == item.ItemId);
                    if (respectivePrice != null)
                    {
                        item.SellPrice = respectivePrice.Sells.UnitPrice;
                        item.SellListingQuantity = respectivePrice.Sells.Quantity;
                        item.BuyPrice = respectivePrice.Buys.UnitPrice;
                        item.BuyOrderQuantity = respectivePrice.Buys.Quantity;
                        item.CommerceDataLastUpdated = DateTime.Now;
                    }
                }
            }
            catch (WebException ex)
            {
                _notifyService.Notify(WebExceptionErrorMessage);
            }
            catch (JsonException ex)
            {
                _notifyService.Notify(JsonExceptionErrorMessage);
            }
        }

        public void UpdateListings(Model.GameItemModel item)
        {
            try
            {
                var updatedData = _tpWrapper.Listings(item.ItemId);
                item.Listing = new ApiItemListing
                {
                    Id = item.ItemId,
                    Buys = updatedData.Buys,
                    Sells = updatedData.Sells
                };
                item.CommerceDataLastUpdated = DateTime.Now;
            }
            catch (WebException ex)
            {
                _notifyService.Notify(WebExceptionErrorMessage);
            }
            catch (JsonException ex)
            {
                _notifyService.Notify(JsonExceptionErrorMessage);
            }
        }
    }
}
