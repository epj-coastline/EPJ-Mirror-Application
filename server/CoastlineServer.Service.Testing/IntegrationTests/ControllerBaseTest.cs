using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoastlineServer.Service.Testing.TestHelper;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace CoastlineServer.Service.Testing.IntegrationTests
{
    public class ControllerBaseTest
    {
        protected readonly HttpClient _client;
        protected readonly string _accessToken;
        protected readonly AuthenticationHeaderValue _authenticationHeader;

        public ControllerBaseTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
            _accessToken = Auth0Helper.GetAccessToken();
            _authenticationHeader = new AuthenticationHeaderValue("Bearer", _accessToken);
        }

        protected HttpRequestMessage CreateHttpRequest(HttpMethod method, string requestUri, object content)
        {
            var request = CreateHttpRequest(method, requestUri);
            var serializeContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8,
                "application/json");
            request.Content = serializeContent;
            return request;
        }

        protected HttpRequestMessage CreateHttpRequest(HttpMethod method, string requestUri)
        {
            var request = new HttpRequestMessage(method, requestUri);
            request.Headers.Authorization = _authenticationHeader;
            return request;
        }

        protected async Task<T> GetRequestData<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
    }
}