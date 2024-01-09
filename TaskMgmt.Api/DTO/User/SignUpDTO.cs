using System.ComponentModel.DataAnnotations;

namespace TaskMgmt.Api.DTO
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, ErrorMessage = "Password must be between 6-20 characters ", MinimumLength = 6)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "GroupName is required.")]
        [StringLength(100, ErrorMessage = "GroupName must be less than 100 characters.", MinimumLength = 1)]
        public string GroupName { get; set; }
    }
}