using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            return (await _context.Categories
                                    .Include(c => c.ParentCategory)
                                    .ToListAsync())
                                    .Where(c => c.ParentCategory == null);
        }
        
        public async Task<bool> AddCategoryAsync(Category category){
            _context.Categories.Add(category);
            return (await _context.SaveChangesAsync()>0);
        }

        public async Task<Category> GetCategoryAsync(int id){
            return await _context.Categories.
                FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteCategoryAsync(Category category){
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync()>0;
        }
        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

    }
}