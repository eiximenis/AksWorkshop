using BeerBook.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web.Clients
{
    public interface IBasketClient
    {
        Task<BasketModel> GetBasket(string user);
        Task AddBeerToBasket(string user, int id);
    }
}
