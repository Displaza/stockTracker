using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fin_backend.Models;
using fin_backend.Services;
using fin_backend.Models.FinModels;

namespace fin_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IFinDataService fds) : ControllerBase
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

        [HttpPost]
        [Route("searchSymbol")]
        public async Task<IActionResult> SearchSymbol([FromBody]SymbolsDTO symbol)
        {
            List<Symbol> searchResult = await fds.SearchForSymbol(symbol.symbol);
            return Ok(searchResult);
        }
    }
}
