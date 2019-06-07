using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using OzeContract.Communication;
using Newtonsoft.Json;

namespace OzeCommuncation
{
    public class CustomHttpClient : ICustomHttpClient
    {
        public async Task<Response<T>> Get<T>(Request request) where T : class
        {
            var response = new Response<T>();

            if (request != null)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        var result = await client.GetAsync(request.Url);

                        return await GetResponseData<T>(result);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }

            return response;
        }

        private async Task<Response<T>> GetResponseData<T>(HttpResponseMessage result) where T : class
        {
            var response = new Response<T>();

            if (result != null && result.IsSuccessStatusCode)
            {
                var contentAsString = await result.Content.ReadAsStringAsync();

                T content = JsonConvert.DeserializeObject<T>(contentAsString);

                return new Response<T>(content);
            }

            return response;
        }
    }
}