using System.ComponentModel.DataAnnotations;
namespace CleanArchitectureExample.WebApi2.Presentation
{
    public class UserRegistrationRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public required string Email { get; set; }

    }
}
