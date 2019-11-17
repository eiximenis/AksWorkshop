using Microsoft.AspNetCore.Mvc;

namespace BeerBook.Catalog.Controllers
{

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");

        }
    }
}

