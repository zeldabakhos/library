using Microsoft.EntityFrameworkCore;
using project.Models;
using project.DTOs;

namespace project.Services
{
    public class BookService
    {
        private readonly Library_AppContext _context;

        public BookService()
        {
            var options = new DbContextOptionsBuilder<Library_AppContext>()
                .UseSqlServer("Server=tcp:epita1.database.windows.net,1433;Initial Catalog=database1;Persist Security Info=False;User ID=zelda;Password=Z@B18Juin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
                .Options;

            _context = new Library_AppContext(options);
        }
        
        public async Task<List<BookReadDTO>> GetAllBooks()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher) // NEW: include publisher relation
                .Select(b => new BookReadDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Summary = b.Summary,
                    PublishedDate = b.PublishedDate,
                    AuthorName = b.Author.Name,
                    PublisherName = b.Publisher != null ? b.Publisher.Name : null // NEW
                })
                .ToListAsync();
        }

        public async Task<BookReadDTO?> GetBookById(int id)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher) // NEW: include publisher relation
                .Where(b => b.Id == id)
                .Select(b => new BookReadDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Summary = b.Summary,
                    PublishedDate = b.PublishedDate,
                    AuthorName = b.Author.Name,
                    PublisherName = b.Publisher != null ? b.Publisher.Name : null // NEW
                })
                .FirstOrDefaultAsync();
        }

        public async Task<string> AddBook(BookCreateDTO dto)
        {
            if (dto.PublishedDate > DateTime.Now)
                return "A book cannot have a future publication date.";

            var authorExists = await _context.Authors.AnyAsync(a => a.Id == dto.AuthorId);
            if (!authorExists)
                return "Invalid AuthorId.";

            var book = new Book
            {
                Title = dto.Title,
                Summary = dto.Summary,
                PublishedDate = dto.PublishedDate,
                AuthorId = dto.AuthorId,
                PublisherId = dto.PublisherId 
            };


            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return "Book added successfully.";
        }

        public async Task<string> UpdateBook(int id, BookCreateDTO dto)
        {
            var existing = await _context.Books.FindAsync(id);
            if (existing == null)
                return "Book not found.";

            existing.Title = dto.Title;
            existing.Summary = dto.Summary;
            existing.PublishedDate = dto.PublishedDate;
            existing.AuthorId = dto.AuthorId;

            await _context.SaveChangesAsync();
            return "Book updated successfully.";
        }

        public async Task<string> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return "Book not found.";

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return "Book deleted successfully.";
        }
    }
}
