

using BeerBook.Basket.Proto;
using BeerBook.Models.Responses;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using static BeerBook.Basket.Proto.Basket;
using Grpc.Core;
using Microsoft.Extensions.Options;

namespace BeerBook.Web.Clients
{
    public class BasketGrpcClient : IBasketClient
    {
        private readonly ILogger _logger;
        private string _url;
        public BasketGrpcClient(IOptions<Settings> settings, ILogger<BasketClient> logger)
        {
            _url = settings.Value.Urls.BasketGrpc;
            _logger = logger;
        }

        public async Task<BasketModel> GetBasket(string user)
        {
            _logger.LogInformation($">>> Begin gRPC to call for get basket of user {user}. Base url is {_url}");
            var channel = GrpcChannel.ForAddress(_url);
            var client = new BasketClient(channel);
            var request = new UserBasketRequest() { User = user };
            var model = new BasketModel()
            {
                User = user,
                Beers = Array.Empty<int>()
            };

            try
            {
                var response = await client.GetByUserAsync(request);
                model.User = response.User;
                model.Beers = response.Beers.ToArray();
            }
            catch (RpcException ex)
            {
                _logger.LogError(ex, "!!! Error returned from gRPC service -> Returning empty basket");
            }

            _logger.LogInformation($"<<< Ended gRPC call for get basket of user {user}");
            return model;
        }

        public async Task AddBeerToBasket(string user, int id)
        {
            _logger.LogInformation($">>> Begin gRPC call for add beer {id} to user {user} .Base url is {_url}");
            var channel = GrpcChannel.ForAddress(_url);
            var client = new BasketClient(channel);
            var request = new UpdateUserBasketRequest()
            {
                User = user,
                BeerId = id
            };

            var response = await client.UpdateFromUserAsync(request);
            _logger.LogInformation($"<<< Ended gRPC call for add beer {id} to user {user}");
        }
    }
}
