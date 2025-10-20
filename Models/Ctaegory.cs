using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
        public string? Description { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }

    public class BookCategory
    {
        public int BookId { get; set; }
        public Book? Book { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}