using Newtonsoft.Json;
using System.Text;
using ParkingLot.Models;

namespace ParkingLot.ApiManager
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<T> PostAsyncDeserialized<T>(string url, object data) where T : IModel
        {
            string jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }

            else return default(T);
        }

        public async Task<T> GetAsync<T>(string url) where T : IModel
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            else return default(T);
        }

        public async Task<T> GetAsyncIEnumerable<T>(string url) where T : IEnumerable<IModel>
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            else return default(T);
        }

        public async Task<T> DeleteAsyncIEnumerable<T>(string url) where T : IEnumerable<IModel>
        {
            HttpResponseMessage response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            else return default(T);
        }

        public async Task<T> DeleteAsync<T>(string url) where T : IModel
        {
            HttpResponseMessage response = await _client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseBody);
            }
            else return default(T);
        }
    }
}
