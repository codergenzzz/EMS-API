using EMS_API.Dtos.Request;
using EMS_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace EMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await Task.Run(() => _authenticationService.Register(account));

            if (result == false)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await Task.Run(() => _authenticationService.Login(request));
            if (result.Token == null)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }
    }
}
