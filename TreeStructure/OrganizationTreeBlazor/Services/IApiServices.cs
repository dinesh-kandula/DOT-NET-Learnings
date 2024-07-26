using System.Net.Http;
using System.Text.Json;
using System.Text;
using TreeDomainLibrary.Models;

namespace OrganizationTreeBlazor.Services
{
    public interface IApiServices
    {
        Task<List<T>> GetApiData<T>(string url);
        Task<string> PostAsync<I>(string url, I objectData);
        Task<string> PutAsync<T>(string url, T objectData);
        Task<string> DeleteAsync<T>(string url, T objectData);
    }
}
