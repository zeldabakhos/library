using Microsoft.EntityFrameworkCore;
using project.Models;
using project.DTOs;

namespace project.Services
{
    public class CategoryService
    {
        private readonly Library_AppContext _context;

        public CategoryService(Library_AppContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryReadDTO>> GetAllCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                .ToListAsync();

            return categories.Select(c => new CategoryReadDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Books = c.BookCategories?.Select(bc => new BookReadDTO
                {
                    Id = bc.Book.Id,
                    Title = bc.Book.Title,
                    Summary = bc.Book.Summary,
                    PublishedDate = bc.Book.PublishedDate
                }).ToList()
            }).ToList();
        }

        public async Task<CategoryReadDTO?> GetCategoryById(int id)
        {
            var c = await _context.Categories
                .Include(c => c.BookCategories)
                .ThenInclude(bc => bc.Book)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (c == null) return null;

            return new CategoryReadDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Books = c.BookCategories?.Select(bc => new BookReadDTO
                {
                    Id = bc.Book.Id,
                    Title = bc.Book.Title,
                    Summary = bc.Book.Summary,
                    PublishedDate = bc.Book.PublishedDate
                }).ToList()
            };
        }

        public async Task<string> AddCategory(CategoryCreateDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return "Category added successfully.";
        }

        public async Task<string> UpdateCategory(int id, CategoryCreateDTO dto)
        {
            var existing = await _context.Categories.FindAsync(id);
            if (existing == null)
                return "Category not found.";

            existing.Name = dto.Name;
            existing.Description = dto.Description;

            await _context.SaveChangesAsync();
            return "Category updated successfully.";
        }

        public async Task<string> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return "Category not found.";

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return "Category deleted successfully.";
        }
    }
}
