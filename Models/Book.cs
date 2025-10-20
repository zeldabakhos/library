using System;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 150 characters.")]
        public string Title { get; set; }

        [StringLength(2000, ErrorMessage = "Summary cannot exceed 2000 characters.")]
        public string Summary { get; set; }

        [DataType(DataType.Date)]
        [CustomValidation(typeof(Book), nameof(ValidatePublishedDate))]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "An author must be assigned to the book.")]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }
        
        [Required(ErrorMessage = "A publisher must be assigned to the book.")]
        public int? PublisherId { get; set; }   // make it nullable if you want it optional

        public Publisher? Publisher { get; set; }  // navigation property

        // --- Custom Validator for PublishedDate ---
        public static ValidationResult? ValidatePublishedDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
                return new ValidationResult("Published date cannot be in the future.");

            if (date.Year < 1900)
                return new ValidationResult("Published date must be after 1900.");

            return ValidationResult.Success;
        }
    }
}