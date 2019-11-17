using BeerBook.Web.Clients;
using BeerBook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Web.Controllers
{

    public class BasketController : Controller
    {
        private readonly ICatalogClient _catalogClient;
        private readonly IBasketClient _basketClient;
        private readonly string _userName;
        public BasketController(ICatalogClient catalogClient, IBasketClient basketClient, IConfiguration config)
        {
            _catalogClient = catalogClient;
            _basketClient = basketClient;
            _userName = config["userName"] ?? "defaultUser";
        }

        [ActionName("AddBeer")]
        public async Task<IActionResult> GetAddView(int id)
        {
            var beer = await _catalogClient.GetBeer(id);
            var basket = await _basketClient.GetBasket(_userName);
            return View("Add", new AddBasketViewModel
            { 
                BeerToAdd = beer,
                CurrentBasket = basket
            });
        }

        [ActionName("AddBeer")]
        [HttpPost]
        public async Task<IActionResult> AddBeerToBasket(int id)
        {
            await _basketClient.AddBeerToBasket(_userName, id);
            return RedirectToAction("Index", "Home");
        }


    }
}
