using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using NetwiseApp.Models;



namespace NetwiseApp.Services
{
    public class CatFactService
    {
        private readonly HttpClient _httpClient;

        public CatFactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetCatFact()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://catfact.ninja/fact");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var factObject = JsonSerializer.Deserialize<CatFactResponse>(responseData);
                if (factObject != null && factObject.Fact != null)
                {
                    return factObject.Fact;
                }
            }
            return null;
        }
    }
}
