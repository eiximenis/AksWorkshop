using BeerBook.Models.Requests;
using BeerBook.Models.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BeerBook.Web.Clients
{
    public class OrderClient : IOrderClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        public OrderClient(HttpClient httpClient, ILogger<OrderClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<int> SubmitOrder(OrderRequest order)
        {
            var url = "api/Orders";
            var response = await _httpClient.PostAsJsonAsync(url, order);
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadAsAsync<int>();
            return id;
        }

        public async Task<PagedResponse<OrderListItem>> GetOrdersFrom(string user)
        {
            var url = $"api/Orders/{user}?page=1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsAsync<PagedResponse<OrderListItem>>();
            return data;
        }
    }
}
