namespace project.DTOs
{
    public class PublisherCreateDTO
    {
        public string Name { get; set; }
        public string? Location { get; set; }
    }

    public class PublisherReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }
        public List<BookReadDTO>? Books { get; set; }
    }
}