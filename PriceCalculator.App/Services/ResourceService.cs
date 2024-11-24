using PriceCalculator.App.Model;
using System.Net.Http;
using System.Net.Http.Json;

namespace PriceCalculator.App.Services
{
    public class ResourceService
    {
        private readonly HttpClient _httpClient;
        public string Url { get; set; } = "https://localhost:7072/";

        public ResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ResourceDTO>> GetResourcesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ResourceDTO>>($"{Url}api/resources");
        }
        public async Task<ResourceDTO> GetRseourceByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<ResourceDTO>($"{Url}api/Resources/{id}");

        public async Task CreateRseourceAsync(ResourceDTO resource)
        {
            var response = await _httpClient.PostAsJsonAsync($"{Url}api/resources", resource);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating resource: {errorContent}");
            }
        }

        public async Task UpdateRseourceAsync(ResourceDTO resource) =>
            await _httpClient.PutAsJsonAsync($"{Url}api/Resources/{resource.Id}", resource);

        public async Task DeleteRseourceAsync(int id) =>
            await _httpClient.DeleteAsync($"{Url}api/Resources/{id}");

    }
}
