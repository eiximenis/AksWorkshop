using BeerBook.Catalog.Data;
using BeerBook.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace BeerBook.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private const int PageSize = 10;
        private readonly ILogger _logger;
        private readonly CatalogContext _db;
        public BeersController(CatalogContext db, ILogger<BeersController> logger)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1)
        {
            var from = ((page - 1) * PageSize) + 1;
            var to = from + (PageSize - 1);
            var data = await _db.Beers.Include("Brewery").Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();

            var response = new PagedResponse<BeerListItem>(from, to,
                data.Select(b => new BeerListItem
                {
                    BreweryId = b.BreweryId,
                    BreweryName = b.Brewery.Name,
                    Id = b.Id,
                    Name = b.Name,
                    Style = b.Style
                }));

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var beer = await _db.Beers.Include("Brewery").SingleOrDefaultAsync(b => b.Id == id);

            if (beer == null)
            {
                return NotFound(new { Message = $"Beer {id} not found" });
            }

            var beerItem = new BeerItem()
            {
                Name = beer.Name,
                Brewery = beer.Brewery.Name,
                Abv = beer.Abv,
                Ibus = beer.Ibus,
                Style = beer.Style,
                Id = beer.Id
            };

            return Ok(beerItem);
        }
    }
}
