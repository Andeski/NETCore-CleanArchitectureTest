using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Interfaces;
using CleanArchitectureExample.Application.Mappers;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(UserDto userDto)
        {
            try
            {
                if (await _userRepository.EmailExistsAsync(userDto.Email))
                {
                    return false;
                }

                var user = UserMapper.MapToEntity(userDto);
                user.Id = Guid.NewGuid();

                await _userRepository.AddAsync(user);
                return true;
            }
            catch (ApplicationException)
            {
                return false;
            }
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user != null ? UserMapper.MapToDto(user) : null;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepository.EmailExistsAsync(email);
        }
    }
}