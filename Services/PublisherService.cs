using Microsoft.EntityFrameworkCore;
using project.Models;
using project.DTOs;

namespace project.Services
{
    public class PublisherService
    {
        private readonly Library_AppContext _context;

        public PublisherService(Library_AppContext context)
        {
            _context = context;
        }

        public async Task<List<PublisherReadDTO>> GetAllPublishers()
        {
            var publishers = await _context.Publishers
                .Include(p => p.Books)
                .ToListAsync();

            return publishers.Select(p => new PublisherReadDTO
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                Books = p.Books?.Select(b => new BookReadDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Summary = b.Summary,
                    PublishedDate = b.PublishedDate
                }).ToList()
            }).ToList();
        }

        public async Task<PublisherReadDTO?> GetPublisherById(int id)
        {
            var p = await _context.Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null) return null;

            return new PublisherReadDTO
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                Books = p.Books?.Select(b => new BookReadDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    Summary = b.Summary,
                    PublishedDate = b.PublishedDate
                }).ToList()
            };
        }

        public async Task<string> AddPublisher(PublisherCreateDTO dto)
        {
            var publisher = new Publisher
            {
                Name = dto.Name,
                Location = dto.Location
            };

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return "Publisher added successfully.";
        }

        public async Task<string> UpdatePublisher(int id, PublisherCreateDTO dto)
        {
            var existing = await _context.Publishers.FindAsync(id);
            if (existing == null)
                return "Publisher not found.";

            existing.Name = dto.Name;
            existing.Location = dto.Location;

            await _context.SaveChangesAsync();
            return "Publisher updated successfully.";
        }

        public async Task<string> DeletePublisher(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return "Publisher not found.";

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return "Publisher deleted successfully.";
        }
    }
}
