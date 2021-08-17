using System.Collections.Generic;
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
    public class ProductController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public ProductController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromQuery] GetCategoryPageDto categoryPageDto)
        {
            return await _dataContext.Products.Include(p => p.ProductImgs)
                .Where(p => p.CategoryId == categoryPageDto.CategoryId)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailedProductDto>> GetById(int id)
        {
            return await _dataContext.Products.Include(p => p.ProductImgs)
                .Include(p => p.ProductFeatures).ThenInclude(f => f.Feature)
                .Include(p => p.RealProducts)
                .Where(p => p.Id == id)
                .ProjectTo<DetailedProductDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}