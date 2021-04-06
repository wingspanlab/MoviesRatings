using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoviesRatingsApp.Common
{
    public class HttpCaller<T>
    {
        private readonly HttpClient Client;

        public HttpCaller(HttpClient client, string baseAddress = "")
        {
            if (!string.IsNullOrEmpty(baseAddress))
            {
                client.BaseAddress = new Uri(baseAddress);
            }
            Client = client;
        }

        public async Task<T> CallGetAsync(string url = "")
        {
            T result;
            try
            {
                var response = await Client.GetAsync(
                    url);

                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    result = await JsonSerializer.DeserializeAsync
                        <T>(responseStream);
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
                //logging
            }
        }
    }
}
