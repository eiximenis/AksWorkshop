using BeerBook.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web.Clients
{
    public interface ICatalogClient
    {
        Task<PagedResponse<BeerListItem>> GetBeers(int page);
        Task<BeerItem> GetBeer(int id);
    }
}
