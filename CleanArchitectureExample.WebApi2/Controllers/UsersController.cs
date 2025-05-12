using CleanArchitectureExample.Application.Dtos;
using CleanArchitectureExample.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRegistrationService _registrationService;

        public UsersController(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] UserDto userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.FirstName))
            {
                return BadRequest("Invalid user data.");
            }

            var emailExists = await _registrationService.EmailExistsAsync(userDto.Email);
            if (emailExists)
            {
                return BadRequest("Email already exists.");
            }

            var success = await _registrationService.RegisterUserAsync(userDto);
            if (!success)
            {
                return BadRequest("User registration failed.");
            }

            return CreatedAtAction(nameof(GetUserByEmail), new { email = userDto.Email }, userDto);
        }

        [HttpGet("by-email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var userDto = await _registrationService.GetUserByEmailAsync(email);
            if (userDto == null)
            {
                return NotFound("User not found.");
            }

            return Ok(userDto);
        }
    }
}