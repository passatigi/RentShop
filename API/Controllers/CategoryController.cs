using System.Collections;
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
    //[ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        public CategoryController(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var allCategories = await _dataContext.Categories.Include(c => c.ParentCategory).Where(c => c.ParentCategory == null)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
            

            return Ok(allCategories);
        }
        [HttpPost]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> AddCategory(CategoryDto categoryDto)
        {
            var allCategories = await _dataContext.Categories.Include(c => c.ParentCategory).Where(c => c.ParentCategory == null)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToListAsync();
            

            return Ok(allCategories);
        }
    }
}