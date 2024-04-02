namespace Mango.Services.AuthAPI.Dtos
{
    public record UserDto(string Id, string Email, string Name, string PhoneNumber);
    public record RegisterRequestDto(string Email, string Name, string PhoneNumber, string Password);
    public record LoginRequestDto(string UserName, string Password);
    public record LoginResponseDto(UserDto User, string Token);
}
