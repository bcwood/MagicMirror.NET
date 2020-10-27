using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MagicMirror.Services
{
    public class ApiClient
    {
        public static async Task<T> GetAsync<T>(string url)
        {
            T result = default(T);

            using (var client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);

                if (!string.IsNullOrWhiteSpace(response))
                {
                    result = JsonConvert.DeserializeObject<T>(response);
                }

                return result;
            }            
        }
    }
}
