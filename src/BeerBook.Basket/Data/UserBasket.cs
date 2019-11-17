using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Basket.Data
{
    public class UserBasket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("User")]
        public string UserName { get; set; }

        [BsonElement("Beers")]
        public List<int> BeerIds { get; set; }

        [BsonElement("UpdatedAt")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime LastUpdated { get; set; }


    }
}
