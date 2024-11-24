using PriceCalculator.App.Model;
using System.Net.Http;
using System;
using System.Net.Http.Json;

namespace PriceCalculator.App.Services
{

    public class PriceService
    {
        private readonly HttpClient _httpClient;
        public string Url { get; set; } = "https://localhost:7072/";

        public PriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> GetTotalPriceAsync(PriceCalculationRequestDTO requestPayload)
        {
            // Send POST request to the API endpoint
            var response = await _httpClient.PostAsJsonAsync($"{Url}api/MSPTiers/CalculatePrice", requestPayload);

            // Ensure the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read and return the response as a decimal value
                var totalPrice = await response.Content.ReadFromJsonAsync<decimal>();
                return totalPrice;
            }

            // Handle the error scenario
            throw new Exception($"Failed to calculate price: {response.ReasonPhrase}");
        }

    }
}
