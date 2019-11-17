using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerBook.Basket.Services;
using BeerBook.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BeerBook.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IUserBasketService _svc;
        public BasketController(IUserBasketService svc, ILogger<BasketController> logger)
        {
            _logger = logger;
            _svc = svc;
        }

        // GET api/values
        [HttpGet("{user}")]
        public async Task<IActionResult> GetByUser(string user)
        {
            var basket = await _svc.GetUserBasketByUser(user);
            if (basket == null)
            {
                return NotFound(new { Message = $"User {user} do not have any basket" });
            }

            return Ok(new BasketModel()
            {
                User = basket.UserName,
                Beers = basket.BeerIds.ToArray()
            });
        }

        [HttpDelete("{user}")]
        public async Task<IActionResult> DeleteByUser(string user)
        {
            var found = await _svc.DeleteFromUser(user);
            return found ? (IActionResult)NoContent() : (IActionResult)NotFound();
        }


        [HttpPost("{user}/beers/{beerId:int}")]
        public async Task<IActionResult> UpdateFromUser(string user, int beerId)
        {
            await _svc.UpdateBasketFromUser(user, beerId);
            return Ok();
        }
    }
}
