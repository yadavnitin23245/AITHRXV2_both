using System.ComponentModel.DataAnnotations;

namespace Airthwholesale.Bal.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
