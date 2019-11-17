using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Order.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string User { get; set; }
        public ICollection<OrderLine> Lines { get; set; }

        public DateTime PurchaseDate { get; set; }

        public Order()
        {
            Lines = new List<OrderLine>();
        }
    }
}
