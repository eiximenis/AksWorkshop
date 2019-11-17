using BeerBook.Basket.Proto;
using BeerBook.Basket.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BeerBook.Basket.Proto.Basket;

namespace BeerBook.Basket.GrpcServices
{
    public class BasketService : BasketBase
    {

        private readonly ILogger _logger;
        private readonly IUserBasketService _svc;
        public BasketService(IUserBasketService svc, ILogger<BasketService> logger)
        {
            _logger = logger;
            _svc = svc;
        }

        public override async Task<EmptyResponse> UpdateFromUser(UpdateUserBasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation(">>> Begin BasketService.UpdateFromUser gRPC method.");
            await _svc.UpdateBasketFromUser(request.User, request.BeerId);
            _logger.LogInformation("<<< Ended BasketService.UpdateFromUser gRPC method.");
            return new EmptyResponse();
        }


        public override async Task<BasketResponse> GetByUser(UserBasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation(">>> Begin BasketService.GetByUser gRPC method.");
            var user = request.User;
            var basket = await _svc.GetUserBasketByUser(user);
            if (basket == null)
            {
               throw new Exception ($"User {user} do not have any basket");
            }

            var response = new BasketResponse();
            response.User = basket.UserName;
            response.Beers.AddRange(basket.BeerIds);
            _logger.LogInformation("<<< Ended BasketService.GetByUser gRPC method.");
            return response;
        }

        public override async Task<BasketDeletedResponse> DeleteByUser(UserBasketRequest request, ServerCallContext context)
        {
            _logger.LogInformation(">>> Begin BasketService.DeleteByUser gRPC method.");
            var found = await _svc.DeleteFromUser(request.User);
            var response = new BasketDeletedResponse() { Deleted = found };
            _logger.LogInformation("<<< Ended BasketService.DeleteByUser gRPC method.");
            return response;

        }

    }
}
