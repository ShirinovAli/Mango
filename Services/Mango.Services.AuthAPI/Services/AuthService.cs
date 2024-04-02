using Mango.Services.AuthAPI.Data;
using Mango.Services.AuthAPI.Dtos;
using Mango.Services.AuthAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto requestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == requestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, requestDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDto(null, string.Empty);
            }

            string token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new(user.Id, user.Email, user.Name, user.PhoneNumber);

            LoginResponseDto responseDto = new(userDto, token);

            return responseDto;
        }

        public async Task<string> Register(RegisterRequestDto requestDto)
        {
            ApplicationUser user = new()
            {
                UserName = requestDto.Email,
                Email = requestDto.Email,
                NormalizedEmail = requestDto.Email.ToUpper(),
                Name = requestDto.Name,
                PhoneNumber = requestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, requestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == requestDto.Email);
                    UserDto userDto = new(
                                          userToReturn.Id,
                                          userToReturn.Email,
                                          userToReturn.Name,
                                          userToReturn.PhoneNumber
                                          );


                    return string.Empty;
                }
                else
                {
                    return result.Errors.First().Description;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "Error Encountered";
        }
    }
}
