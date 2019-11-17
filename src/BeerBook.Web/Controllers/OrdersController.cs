using BeerBook.Models.Requests;
using BeerBook.Web.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace BeerBook.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderClient _orderClient;
        private readonly IBasketClient _basketClient;
        private readonly string _userName;
        public OrdersController(IOrderClient orderClient, IBasketClient basketClient, IConfiguration config)
        {
            _orderClient = orderClient;
            _basketClient = basketClient;
            _userName = config["userName"] ?? "defaultUser";
        }

        [ActionName("Purchase")]
        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            var basket = await _basketClient.GetBasket(_userName);
            if (basket == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var request = new OrderRequest()
            {
                UserName = _userName
            };

            request.Lines.AddRange(
                basket.Beers.GroupBy(x => x).Select(g => new OrderRequestLine
                {
                    BeerId = g.Key,
                    Qty = g.Count()
                })
            );

           await _orderClient.SubmitOrder(request);

            return RedirectToAction("List");
        }

        [ActionName("List")]
        public async Task<IActionResult> OrderList()
        {
            var orders = await _orderClient.GetOrdersFrom(_userName);
            return View(orders);
        }
    }
}
