using BeerBook.Basket.Data;
using System.Threading.Tasks;

namespace BeerBook.Basket.Services
{
    public interface IUserBasketService
    {
        Task<UserBasket> GetUserBasketByUser(string user);
        Task UpdateBasketFromUser(string user, int beerToAdd);
        Task<bool> DeleteFromUser(string user);
    }
}