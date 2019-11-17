using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        // GET api/values
        [HttpGet("value")]
        public ActionResult<int> Get()
        {
            /*
             // CODIGO ORIGINAL DE LA API ERRONEA
             return 42;
            */
            return new Random().Next(1,101) * 100;
        }
    }
}
