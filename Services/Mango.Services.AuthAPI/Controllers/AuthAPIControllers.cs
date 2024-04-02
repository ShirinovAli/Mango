using Mango.Services.AuthAPI.Dtos;
using Mango.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIControllers : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthAPIControllers(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var errorMessage = await _authService.Register(dto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var loginResponse = await _authService.Login(dto);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }
    }
}
