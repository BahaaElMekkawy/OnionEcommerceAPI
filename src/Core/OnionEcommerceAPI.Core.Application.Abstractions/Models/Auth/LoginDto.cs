using System.ComponentModel.DataAnnotations;

namespace OnionEcommerceAPI.Core.Application.Abstractions.Models.Auth
{
    public class LoginDto
    {
        [Required]
        public required string Email { get; set; }
        /***********************************************************/
        [Required]
        public required string Password { get; set; }
        /***********************************************************/
    }
}
