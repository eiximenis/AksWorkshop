using System;
using System.Collections.Generic;
using System.Text;

namespace BeerBook.Models.Requests
{
    public class OrderRequest
    {
        public string UserName { get; set; }
        public List<OrderRequestLine> Lines { get; set; }

        public OrderRequest()
        {
            Lines = new List<OrderRequestLine>();
        }
    }

    public class OrderRequestLine
    {
        public int BeerId { get; set; }
        public int Qty { get; set; }
    }
}
