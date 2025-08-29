using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fin_backend.Models;

namespace fin_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("You've reached the backend traveller!");
        }

        [HttpPost]
        [Route("ColourPost")]
        public IActionResult ColourTest([FromBody]ColourModel colourModel)
        {
            return Ok("Colour is: " + colourModel.Colour);
        }
    }
}
