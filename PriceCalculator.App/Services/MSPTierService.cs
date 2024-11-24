using PriceCalculator.App.Model;
using System.Net.Http;
using System.Net.Http.Json;

namespace PriceCalculator.App.Services
{
    public class MSPTierService
    {
        private readonly HttpClient _httpClient;
        public string Url { get; set; } = "https://localhost:7072/";

        public MSPTierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<MSPTierDTO>> GetMSPTiersAsync() =>
            await _httpClient.GetFromJsonAsync<List<MSPTierDTO>>($"{Url}api/MSPTiers");

        public async Task<MSPTierDTO> GetMSPTierByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<MSPTierDTO>($"{Url}api/MSPTiers/{id}");

        public async Task CreateMSPTierAsync(MSPTierDTO mspTier)
        {
            var response = await _httpClient.PostAsJsonAsync($"{Url}api/MSPTiers", mspTier);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating mspTier: {errorContent}");
            }
        }

        public async Task UpdateMSPTierAsync(MSPTierDTO mspTier) =>
            await _httpClient.PutAsJsonAsync($"{Url}api/MSPTiers/{mspTier.Id}", mspTier);

        public async Task DeleteMSPTierAsync(int id) =>
            await _httpClient.DeleteAsync($"{Url}api/MSPTiers/{id}");
        
    }
}
