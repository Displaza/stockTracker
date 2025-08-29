using Microsoft.AspNetCore.Mvc;
using fin_backend.Models;
using fin_backend.Models.DTOs;
using fin_backend.Repos; 
using System.Threading.Tasks;
using fin_backend.Services;
using Microsoft.AspNetCore.Identity;
//using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace fin_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserRepo userRepo, IJwtService JwtGimme) : Controller
    {

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(UserForAuthDto dto)
        {
            //my login logic needs to be better to holy shit hahaha
            User user = await userRepo.GetUserAsync(dto.Username.Trim());

            //if the username OR the password is incorrect, return unauthorized
            if(user == null || 
                new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, dto.Password)
                == PasswordVerificationResult.Failed)
            {
                return Unauthorized("GET YO CREDENTIALS IN ORDA");
            }

            string token = JwtGimme.GenerateJwtToken(user);

            //you need to properly format the JSON you RETARD 
            var response = new AuthResponseDto
            {
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            return Ok(response);
            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForAuthDto dto)
        {
            User user = new User
            {
                Username = dto.Username.Trim(),
                //im hashing the password in the repository. It's a bit weird i know but it still works.
                PasswordHash = dto.Password.Trim() 
            };

            await userRepo.AddAsync(user);


            return Ok();
        }
    }
}
