using System.Text;
using System.Text.Json;

using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.ModelsDTO;

namespace OrganizationTreeBlazor.Services
{
    public class ApiServices : IApiServices
    {
        private readonly HttpClient _httpClient;

        public ApiServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> GetApiData<T>(string url)
        {
            using var response = await _httpClient.GetAsync(url);
            string content = await response.Content.ReadAsStringAsync();
            //await Console.Out.WriteLineAsync(content);
            var settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All
            };
            List<T> result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(content, settings)!;
            return result;
        }

        public async Task<string> PostAsync<I>(string url, I objectData)
        {
            using MultipartFormDataContent formContent = new MultipartFormDataContent();
            foreach (var property in typeof(I).GetProperties())
            {
                formContent.Add(new StringContent(property.GetValue(objectData)?.ToString()), property.Name);
            }

            using var response = await _httpClient.PostAsync(url, formContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task<string> PutAsync<T>(string url, T objectData)
        {
            using MultipartFormDataContent formContent = new MultipartFormDataContent();
            foreach (var property in typeof(T).GetProperties())
            {
                formContent.Add(new StringContent(property.GetValue(objectData)?.ToString()), property.Name);
            }

            var response = await _httpClient.PutAsync(url, formContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        public async Task<string> DeleteAsync<T>(string url, T objectData)
        {
            using MultipartFormDataContent formContent = [];
            foreach (var property in typeof(T).GetProperties())
            {
                formContent.Add(new StringContent(property.GetValue(objectData)!.ToString()!), property.Name);
            }

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = formContent
            };

            using HttpResponseMessage response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
