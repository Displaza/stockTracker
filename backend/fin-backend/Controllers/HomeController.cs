using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using fin_backend.Models;
using fin_backend.Services;
using fin_backend.Models.FinModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;

namespace fin_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController(IFinDataService fds) : ControllerBase
    {

        //---BASIC TESTING METHODS

        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("You've reached the backend");
        }

        [HttpPost]
        [Route("ColourPost")]
        public IActionResult ColourTest([FromBody]ColourModel colourModel)
        {
            return Ok("Colour is: " + colourModel.Colour);
        }

        //---BASIC TESTING METHODS END

        [HttpGet]
        [Route("searchSymbol")]
        public async Task<IActionResult> SearchSymbol(string symbol)
        {
            if(symbol == null || symbol == "")
            {
                return BadRequest();
            }
            List<Symbol> searchResult = await fds.SearchForSymbol(symbol);
            return Ok(searchResult);
        }

        [HttpGet]
        [Route("getNews")]
        public async Task<IActionResult> GetNews(int page = 1, int pageSize = 10)
        {
            //this definitely should be refactored but I can't quite figure out a clean way to implement this
            //into a service without it feeling super redundant, as I need to get the count as well as the paginated data

            IOrderedQueryable<NewsItem> newsQuery = await fds.GetPaginatedNews(page, pageSize);

            var totalCount = await newsQuery.CountAsync();

            var items = await newsQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                items,
                totalCount,
                page,
                pageSize
            });

        }
    }
}
