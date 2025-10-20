namespace project.DTOs
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedDate { get; set; }
        public int AuthorId { get; set; }
        public int? PublisherId { get; set; }

    }

    public class BookReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? AuthorName { get; set; }
        public string? PublisherName { get; set; } 
    }

}