using System.ComponentModel.DataAnnotations;
using Models.Constants;

namespace Models.DTO.Input
{
    public class UserRegisterInputDto
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(LengthConstants.MaximumUsernameLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = LengthConstants.MinimumUsernameLength)]
        [RegularExpression(RegexPatterns.UsernamePattern, ErrorMessage = "Username can only contain alphanumeric characters and underscore (_)")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(LengthConstants.MaximumPasswordLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = LengthConstants.MinimumPasswordLength)]
        [RegularExpression(RegexPatterns.PasswordPattern, ErrorMessage = "Password must contain at least one lowercase, one uppercase, one number and one special characters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public string Currency { get; set; }
    }
}