using SocialMediaWeb.Dtos;
using SocialMediaWeb.Models;

namespace SocialMediaWeb.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegistrationDto registrationDto);
        Task<UserResponseDto> LoginAsync(UserLoginDto loginDto);
        Task<User> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<User> FindById(int id);
        Task<List<ResponseDto>> GetUserAsync();

    }
}
