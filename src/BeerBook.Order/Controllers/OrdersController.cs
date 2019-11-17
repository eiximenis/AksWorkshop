using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerBook.Models.Requests;
using BeerBook.Models.Responses;
using BeerBook.Order.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeerBook.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _db;
        private const int PageSize = 10;

        public OrdersController(OrderContext db )
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest request)
        {
            var order = new BeerBook.Order.Data.Order
            {
                User = request.UserName,
                PurchaseDate = DateTime.UtcNow
            };
            foreach (var line in request.Lines) {
                order.Lines.Add(new OrderLine()
                {
                    BeerId = line.BeerId,
                    Quantity = line.Qty
                });
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            return Ok(order.Id);
        }

        [HttpGet("{user}")]
        public async Task<IActionResult> GetByUser(string user, int page)
        {
            var from = ((page - 1) * PageSize) + 1;
            var to = from + (PageSize - 1);
            var data = await _db.Orders.Include("Lines").Where(o => o.User == user).Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var response = new PagedResponse<OrderListItem>(from, to,
                data.Select(o => new OrderListItem
                {
                    UserName = o.User,
                    TotalBeers = o.Lines.Sum(l => l.Quantity),
                    PurchasedAt = o.PurchaseDate
                }));

            return Ok(response);
        }
    }
}
