using BeerBook.Basket.Data;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Basket.Services
{
    public class UserBasketService : IUserBasketService
    {
        private readonly Settings _settings;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public UserBasketService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            _client = new MongoClient(_settings.Constr);
            _db = _client.GetDatabase("beerbook");
        }

        public Task<UserBasket> GetUserBasketByUser(string user)
        {
            var coll = _db.GetCollection<UserBasket>("basket");
            var item = coll.AsQueryable().FirstOrDefault(ub => ub.UserName == user);
            return Task.FromResult(item);
        }

        public async Task<bool> DeleteFromUser(string user)
        {
            var coll = _db.GetCollection<UserBasket>("basket");
            var result = await coll.DeleteOneAsync(Builders<UserBasket>.Filter.Eq(ub => ub.UserName, user));
            return result.DeletedCount > 0;
        }

        public async Task UpdateBasketFromUser(string user, int beerToAdd)
        {
            var coll = _db.GetCollection<UserBasket>("basket");
            var item = coll.AsQueryable().FirstOrDefault(ub => ub.UserName == user);

            if (item == null)
            {
                var newBasket = new UserBasket()
                {
                    BeerIds = new List<int> { beerToAdd },
                    UserName = user,
                    LastUpdated = DateTime.UtcNow
                };

                await coll.InsertOneAsync(newBasket);
            }
            else
            {
                await coll.UpdateOneAsync(
                    Builders<UserBasket>.Filter.Eq(ub => ub.Id, item.Id),
                    Builders<UserBasket>.Update.Set(ub => ub.LastUpdated, DateTime.UtcNow).Push(ub => ub.BeerIds, beerToAdd));
            }

        }
    }
}
