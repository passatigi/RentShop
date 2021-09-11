using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(
            int categoryId);

        Task<DetailedProductDto> GetProductById(
            int id);

        Task<RealProductSchedule> GetRealProductShedule(int id);
    }
}