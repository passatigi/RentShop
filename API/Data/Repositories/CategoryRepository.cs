using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetParentCategoriesAsync()
        {
            return (await _context.Categories
                                    .Include(c => c.ParentCategory)
                                    .ToListAsync())
                                    .Where(c => c.ParentCategory == null);
        }

        public async Task<IEnumerable<CategoryDto>> GetChildrenCategoriesAsync()
        {
            return await _context.Categories.Where(c => c.ParentCategoryId != null)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
        }


        public async Task<bool> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories.
                FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> DeleteCategoryAsync(Category category)
        {
            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync() > 0;
        }
        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
        }

    }
}