using PriceCalculator.App.Model;
using System.Net.Http.Json;

namespace PriceCalculator.App.Services
{
    public class DiscountService
    {
        private readonly HttpClient _httpClient;
        public string Url { get; set; } = "https://localhost:7072/";

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DiscountDTO>> GetDiscountsAsync() =>
            await _httpClient.GetFromJsonAsync<List<DiscountDTO>>($"{Url}api/Discounts");

        public async Task<DiscountDTO> GetDiscountByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<DiscountDTO>($"{Url}api/Discounts/{id}");

        public async Task CreateDiscountAsync(DiscountDTO discount)
        {
            var response = await _httpClient.PostAsJsonAsync($"{Url}api/discounts", discount);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating discount: {errorContent}");
            }
        }

        public async Task UpdateDiscountAsync(DiscountDTO discount) =>
            await _httpClient.PutAsJsonAsync($"{Url}api/Discounts/{discount.Id}", discount);

        public async Task DeleteDiscountAsync(int id) =>
            await _httpClient.DeleteAsync($"{Url}api/Discounts/{id}");
    }
}
