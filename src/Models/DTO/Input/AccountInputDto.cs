using System.ComponentModel.DataAnnotations;
using Models.Constants;

namespace Models.DTO.Input
{
    public class AccountInputDto
    {
        [Required]
        [Display(Name = "Account Name")]
        [StringLength(LengthConstants.MaximumNameLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = LengthConstants.MinimumNameLength)]
        [RegularExpression(RegexPatterns.NamePattern, ErrorMessage = "Account Name is invalid.")]
        public string Name { get; set; }
    }
}