using CleanArchitectureExample.Application.Dtos;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<bool> RegisterUserAsync(UserDto userDto);

        Task<bool> EmailExistsAsync(string email);

        Task<UserDto?> GetUserByEmailAsync(string email);
    }
}