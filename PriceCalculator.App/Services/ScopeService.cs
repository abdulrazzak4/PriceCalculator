using System.Net.Http.Json;
using PriceCalculator.App.Model;

namespace PriceCalculator.App.Services
{
    public class ScopeService
    {
        private readonly HttpClient _httpClient;
        public string Url { get; set; } = "https://localhost:7072/";
        public ScopeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // CRUD operations for Scopes
        public async Task<List<ScopeDTO>> GetScopesAsync() =>
            await _httpClient.GetFromJsonAsync<List<ScopeDTO>>($"{Url}api/scopes");

        public async Task<ScopeDTO> GetScopeByIdAsync(int id) =>
            await _httpClient.GetFromJsonAsync<ScopeDTO>($"{Url}api/scopes/{id}");

        public async Task CreateScopeAsync(ScopeDTO scope) =>
            await _httpClient.PostAsJsonAsync($"{Url}api/scopes", scope);

        public async Task UpdateScopeAsync(ScopeDTO scope) =>
            await _httpClient.PutAsJsonAsync($"{Url}api/scopes/{scope.Id}", scope);

        public async Task DeleteScopeAsync(int id) =>
            await _httpClient.DeleteAsync($"{Url}api/scopes/{id}");
    }

}
