using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Clients
{
    class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private const string UrlRandom = "/api/random/value";

        public ApiClient(IHttpClientFactory httpClientFactory, ILogger<ApiClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<int> GetRandomValue()
        {
            var client = _httpClientFactory.CreateClient("api");
            _logger.LogInformation($"Calliing api in Url {client.BaseAddress}/{UrlRandom}");
            var response = await client.GetAsync(UrlRandom);
            response.EnsureSuccessStatusCode();
            var value = await response.Content.ReadAsAsync<int>();
            return value;
        }
    }
}
