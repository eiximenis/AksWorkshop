using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Models.Responses
{
    public class OrderListItem
    {
        public int TotalBeers { get; set; }
        public string UserName { get; set; }
        public DateTime PurchasedAt { get; set; }
    }
}
