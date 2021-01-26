using System.ComponentModel.DataAnnotations;
using Models.Constants;

namespace Models.DTO.Input
{
    public class CategoryInputDto
    {
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(LengthConstants.MaximumNameLength, ErrorMessage = "{0} must be at least {2} and at max {1} characters long.", MinimumLength = LengthConstants.MinimumNameLength)]
        [RegularExpression(RegexPatterns.NamePattern, ErrorMessage = "{0} is invalid.")]
        public string Name { get; set; }
    }
}