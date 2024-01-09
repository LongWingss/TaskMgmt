using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Api.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password must be between 6-20 characters", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
