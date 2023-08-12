using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;
using System.Text.Json;

namespace FreeCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<bool> ApplyDiscount(string discountCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelApplyDiscount()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete()
        {
            var result = await _httpClient.DeleteAsync("baskets");

            return result.IsSuccessStatusCode;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await Get();
            if (basket is not null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems.Add(basketItemViewModel);
            }

            await SaveOrUpdate(basket);
        }


        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();

            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();
            if (basket == null)
            {
                return false;
            }

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);

            if (deleteBasketItem is null)
            {
                return false;
            }

            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

            if (!deleteResult)
            {
                return false;
            }

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = string.Empty;
            }

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {

            var response = await _httpClient.PostAsJsonAsync("baskets", basketViewModel);

            return response.IsSuccessStatusCode;

        }

    }
}
