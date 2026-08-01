using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Dtos.Auth;
using ToDo.Api.Services;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]

        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
        {

            try
            {
                var result = await _authService.RegisterAsync(dto, cancellationToken);
                return Ok(result);

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(
          [FromBody] LoginDto dto,
          CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authService.LoginAsync(dto, cancellationToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }



    }
}
