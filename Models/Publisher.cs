using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Publisher name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Publisher name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string? Location { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}