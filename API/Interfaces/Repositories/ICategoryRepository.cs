using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetParentCategoriesAsync();
        Task<bool> AddCategoryAsync(Category category);
        Task<Category> GetCategoryAsync(int id);
        Task<bool> DeleteCategoryAsync(Category category);
        void UpdateCategory(Category category);
    }
}