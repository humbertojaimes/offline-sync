using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfflineSyncMobileApp.Enums;
using OfflineSyncMobileApp.Interfaces;

namespace OfflineSyncMobileApp.Implementations
{
    public class BaseRestService
    {
        static HttpClient httpClient;

        public BaseRestService(string token)
            : this()
        {
            httpClient.DefaultRequestHeaders.Authorization = new($"bearer",token);
        }

        public BaseRestService()
        {
                httpClient = new();
                httpClient.Timeout = TimeSpan.FromSeconds(80);
        }

        public async Task<IRestResponse<W>> SendRequest<V, W>
            (HttpMethod httpMethod,  V content = null, string uri = "")
            where V : class where W : class
        {
            GenericRestResponse<W> result = new();

            try
            {
                HttpResponseMessage httpResponseMessage = await SendHttpRequest(httpMethod, uri, content);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    result.ResponseMessage = "Ok";
                    result.ResponseStatus = ResponseStatus.Success;
                    result.Response = JsonConvert.DeserializeObject<W>
                        (await httpResponseMessage.Content.ReadAsStringAsync());
                }

            }
            catch (TimeoutException timeout)
            {
                result.ResponseStatus = ResponseStatus.Timeout;
                result.ResponseMessage = "Timeout";
            }
            catch (Exception exception)
            {
                result.ResponseStatus = ResponseStatus.Error;
                result.ResponseMessage = "Error";
            }
            return result;
        }

        protected async Task<HttpResponseMessage> SendHttpRequest<V>(HttpMethod httpMethod, string uri, V content)
        {
            StringContent stringContent = default;
            if (content is not null)
            {
                string jsonContent = JsonConvert.SerializeObject(content);
                stringContent = new(jsonContent, Encoding.UTF8, "application/json");
            }

            HttpRequestMessage httpRequestMessage = new(httpMethod, uri);
            if (stringContent is not null)
                httpRequestMessage.Content = stringContent;
            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            return httpResponseMessage;
        }

    }
}
