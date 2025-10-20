using Microsoft.EntityFrameworkCore;
using project.Models;
using project.DTOs;

namespace project.Services
{
    public class AuthorService
    {
        private readonly Library_AppContext _context;

        public AuthorService(Library_AppContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorReadDTO>> GetAllAuthors()
        {
            var authors = await _context.Authors.Include(a => a.Books).ToListAsync();

            return authors.Select(a => new AuthorReadDTO
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                DateOfBirth = a.DateOfBirth,
                Books = a.Books?.Select(b => new BookReadDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Summary = b.Summary,
                    PublishedDate = b.PublishedDate
                }).ToList()
            }).ToList();
        }

        public async Task<AuthorReadDTO?> GetAuthorById(int id)
        {
            var a = await _context.Authors.Include(a => a.Books)
                                          .FirstOrDefaultAsync(a => a.Id == id);
            if (a == null) return null;

            return new AuthorReadDTO
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography,
                DateOfBirth = a.DateOfBirth,
                Books = a.Books?.Select(b => new BookReadDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Summary = b.Summary,
                    PublishedDate = b.PublishedDate
                }).ToList()
            };
        }

        public async Task<string> AddAuthor(AuthorCreateDTO dto)
        {
            if (dto.DateOfBirth > DateTime.Now)
                return "Date of birth cannot be in the future.";

            var author = new Author
            {
                Name = dto.Name,
                Biography = dto.Biography,
                DateOfBirth = dto.DateOfBirth
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return "Author added successfully.";
        }

        public async Task<string> UpdateAuthor(int id, AuthorCreateDTO dto)
        {
            var existing = await _context.Authors.FindAsync(id);
            if (existing == null)
                return "Author not found.";

            existing.Name = dto.Name;
            existing.Biography = dto.Biography;
            existing.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();
            return "Author updated successfully.";
        }

        public async Task<string> DeleteAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return "Author not found.";

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return "Author deleted successfully.";
        }
    }
}
