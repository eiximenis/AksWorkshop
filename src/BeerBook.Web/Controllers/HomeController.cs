using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerBook.Web.Models;
using BeerBook.Web.Clients;

namespace BeerBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatalogClient _catalogClient;
        public HomeController(ICatalogClient catalogClient)
        {
            _catalogClient = catalogClient;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _catalogClient.GetBeers(1);
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
