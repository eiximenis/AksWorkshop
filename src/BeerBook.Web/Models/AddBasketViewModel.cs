using BeerBook.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web.Models
{
    public class AddBasketViewModel
    {
        public BeerItem BeerToAdd { get; set; }
        public BasketModel CurrentBasket { get; set; }
    }
}
