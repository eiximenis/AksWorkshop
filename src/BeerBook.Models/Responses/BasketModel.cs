using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Models.Responses
{
    public class BasketModel
    {
        public string User { get; set; }
        public int[] Beers { get; set; }
    }
}
