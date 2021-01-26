using System;
using System.ComponentModel.DataAnnotations;
using Models.Constants;

namespace Models.DTO.Input
{
    public class EntryInputDto
    {
        [Required]
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be a positive number.")]
        public decimal Amount { get; set; }
        
        [Required]
        [Display(Name = "DateTime")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        
        [Display(Name = "Note")]
        [StringLength(LengthConstants.MaximumNoteLength, ErrorMessage = "{0} must be at max {1} characters long.")]
        public string Note { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        
        [Required]
        [Display(Name = "Account")]
        public Guid AccountId { get; set; }
    }
}