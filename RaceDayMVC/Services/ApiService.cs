using Newtonsoft.Json;
using System.Text;

namespace RaceDayMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
                _httpClient.BaseAddress =
                  new Uri("http://localhost:5009/api/"); ;

        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return default;

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var json = JsonConvert.SerializeObject(data);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response =
                await _httpClient.PostAsync(endpoint, content);

            return response.IsSuccessStatusCode;
        }    
    }
}
