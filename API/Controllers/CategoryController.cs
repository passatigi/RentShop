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
    public class CategoryController : BaseApiController
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
            var mainCategories = (await _dataContext.Categories
                                    .Include(c => c.ParentCategory)
                                    .ToListAsync())
                                    .Where(c => c.ParentCategory == null);
                                    
                                    
            var dtos = _mapper.Map<IEnumerable<CategoryDto>>(mainCategories);
            return Ok(dtos);
            //
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory(CreateCategoryDto categoryDto)
        {
            Category category = new Category 
            { 
                 ParentCategoryId = categoryDto.ParentCategoryId, 
                 ImgLink = categoryDto.ImgLink, 
                 Name = categoryDto.Name 
            };
            _dataContext.Categories.Add(category);
            
            await _dataContext.SaveChangesAsync();

            return Ok(
                new CategoryDto(){ 
                    Name = category.Name, 
                    ImgLink = category.ImgLink, 
                    Id = category.Id
                });
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var category =  _dataContext.Categories.FirstOrDefault(c => c.Id == categoryId);

            if(category == null) return NotFound();
            
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            var category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryDto.Id);

            if(category == null) return NotFound();
            
            category.ImgLink = categoryDto.ImgLink;
            category.Name = categoryDto.Name;
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}