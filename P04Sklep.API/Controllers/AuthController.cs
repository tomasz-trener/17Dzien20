using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P04Sklep.API.Services.AuthService;
using P05Sklep.Shared;

namespace P04Sklep.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class AuthController : Controller
    {

        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpGet("Ukryta"), Authorize]
        public string Ukryta()
        {
            return "ok";
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceReponse<string>>> Login(UserLogin user)
        {
            // teraz poprosimy seriws o sprawdzenie czy dane się zgdzaja
            // jesli to to wgenerujemy token 


            var respose = await authService.Login(user.Email, user.Password);
            if (!respose.Success)
            {
                return BadRequest(respose);
            }

            return Ok(respose);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceReponse<int>>> Register(UserRegister user)
        {
            var response = await authService.Register(user.Email, user.Password);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        }
}
