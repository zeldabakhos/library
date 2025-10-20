namespace project.DTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class CategoryReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<BookReadDTO>? Books { get; set; }
    }
}