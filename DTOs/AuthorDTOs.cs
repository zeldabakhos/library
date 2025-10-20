namespace project.DTOs
{
    public class AuthorCreateDTO
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class AuthorReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<BookReadDTO>? Books { get; set; }
    }
}