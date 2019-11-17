using BeerBook.Models.Requests;
using BeerBook.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web.Clients
{
    public interface IOrderClient
    {
        Task<int> SubmitOrder(OrderRequest order);
        Task<PagedResponse<OrderListItem>> GetOrdersFrom(string user);
    }
}
