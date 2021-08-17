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
    public class AdminHelperController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper __mapper;
        public AdminHelperController(DataContext dataContext, IMapper _mapper)
        {
            __mapper = _mapper;
            _dataContext = dataContext;

        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return await _dataContext.Categories.Where(c => c.ParentCategoryId != null)
                .ProjectTo<CategoryDto>(__mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("features/{id}")]
        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures(int id)
        {
            return await _dataContext.Features.Where(f => f.CategoryId == id)
                .ProjectTo<FeatureDto>(__mapper.ConfigurationProvider).ToListAsync();
        }
    }
}