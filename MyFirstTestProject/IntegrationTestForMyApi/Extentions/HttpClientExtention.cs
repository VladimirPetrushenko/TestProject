using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IntegrationTestForMyApi.Extentions
{
    public static class HttpClientExtention
    {
        public static async Task<HttpResponseMessage> DeleteAsync<T>(this HttpClient TestClient, T item, string route)
        {
            HttpRequestMessage request = new()
            {
                Content = JsonContent.Create(item),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(route)
            };

            return await TestClient.SendAsync(request);
        }
    }
}
