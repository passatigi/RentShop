using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminHelperController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public AdminHelperController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;

        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            return await _dataContext.Categories.Where(c => c.ParentCategoryId != null)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("features/{id}")]
        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures(int id)
        {
            return await _dataContext.Features.Where(f => f.CategoryId == id)
                .ProjectTo<FeatureDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpPost("features")]
        public async Task<ActionResult<FeatureDto>> AddNewFeature(AddFeatureDto featureDto)
        {
            var feature = _mapper.Map<Feature>(featureDto);
            _dataContext.Add(feature);
            await _dataContext.SaveChangesAsync();
            return Ok(_mapper.Map<FeatureDto>(feature));
        }
    }
}