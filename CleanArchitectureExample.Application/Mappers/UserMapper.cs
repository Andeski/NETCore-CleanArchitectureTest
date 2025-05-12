using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Domain.Entities;

namespace CleanArchitectureExample.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.Name.Split(' ').FirstOrDefault() ?? string.Empty,
                LastName = user.Name.Split(' ').Skip(1).FirstOrDefault() ?? string.Empty
            };
        }

        public static User MapToEntity(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Name = $"{userDto.FirstName} {userDto.LastName}".Trim()
            };
        }
    }
}