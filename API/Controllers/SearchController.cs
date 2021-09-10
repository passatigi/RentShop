using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class SearchController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public SearchController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet("{searchstring}")]
        public async Task<ActionResult> Search(string searchString){
            var categories = await _dataContext.Categories
                .Where(c => c.Name.Contains(searchString))
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var products = await _dataContext.Products
                .Where(c => c.Name.Contains(searchString) || c.Vendor.Contains(searchString))
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(new {Categories = categories, Products = products});
        }
    }
}