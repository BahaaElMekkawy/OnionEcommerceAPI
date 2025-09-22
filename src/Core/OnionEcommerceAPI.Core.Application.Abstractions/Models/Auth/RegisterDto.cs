using System.ComponentModel.DataAnnotations;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Auth
{
    public class RegisterDto
    {
        [Required]
        public required string DisplayName { get; set; }
        /***********************************************************/
        [Required]
        public required string UserName { get; set; }
        /***********************************************************/
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        /***********************************************************/
        [Required]
        public required string PhoneNumber { get; set; }
        /***********************************************************/
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$",
        ErrorMessage = "Password must be at least 6 characters long and include an uppercase, lowercase, digit, and special character.")]
        public required string Password { get; set; }

    }
}
