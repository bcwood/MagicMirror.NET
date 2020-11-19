using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MagicMirror.Services
{
    public class ApiClient
    {
        static JsonSerializerSettings Settings => new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        //public static Task<T> GetAsync<T>(string url, Func<HttpClient> getClient = null) =>
        //    Do.WithRetry(() => GetAsyncImpl<T>(url, getClient));

        public static async Task<T> GetAsync<T>(string url, Func<HttpClient> getClient = null)
        {
            T result = default(T);
            var response = await GetRawAsync(url, getClient);

            if (!string.IsNullOrWhiteSpace(response))
            {
                result = JsonConvert.DeserializeObject<T>(response, Settings);
            }

            return result;
        }

        public static async Task<string> GetRawAsync(string url, Func<HttpClient> getClient)
        {
            getClient = getClient ?? (() => new HttpClient());

            using (var client = getClient?.Invoke())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
