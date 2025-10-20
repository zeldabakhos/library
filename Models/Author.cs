using System;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Author name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Biography cannot exceed 1000 characters.")]
        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(Author), nameof(ValidateDateOfBirth))]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Book>? Books { get; set; }

        // --- Custom Validator for DateOfBirth ---
        public static ValidationResult? ValidateDateOfBirth(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
                return new ValidationResult("Date of birth must be before the current date.");

            if (date.Year < 1800)
                return new ValidationResult("Date of birth must be after the year 1800 (too old to be valid).");

            return ValidationResult.Success;
        }
    }
}