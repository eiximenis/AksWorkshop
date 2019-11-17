using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BeerBook.Models.Responses;
using Microsoft.Extensions.Logging;

namespace BeerBook.Web.Clients
{
    public class CatalogClient : ICatalogClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public CatalogClient(HttpClient httpClient, ILogger<CatalogClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<BeerItem> GetBeer(int id)
        {
            var url = $"api/Beers/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<BeerItem>();
            return data;
        }

        public async Task<PagedResponse<BeerListItem>> GetBeers(int page)
        {
            var url = $"api/Beers?page={page}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<PagedResponse<BeerListItem>>();
            return data;
        }
    }
}
