using Mango.Services.AuthAPI.Dtos;

namespace Mango.Services.AuthAPI.Services
{
    public interface IAuthService
    {
        Task<string> Register(RegisterRequestDto requestDto);
        Task<LoginResponseDto> Login(LoginRequestDto requestDto);
    }
}
