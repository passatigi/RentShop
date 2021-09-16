using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IMapper mapper, IUnitOfWork unitOfWork)
        { 
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
    {
        var parentCategories = await _unitOfWork.CategoryRepository.GetParentCategoriesAsync();
        var dtos = _mapper.Map<IEnumerable<CategoryDto>>(parentCategories);
        return Ok(dtos);
    }

    [HttpGet("{categoryId}")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetById(int categoryId)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryAsync(categoryId);              
        var dto = _mapper.Map<CategoryDto>(category);
        return Ok(dto);
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
        
        if(await _unitOfWork.CategoryRepository.AddCategoryAsync(category))
            return Ok(
                new CategoryDto()
                {
                    Name = category.Name,
                    ImgLink = category.ImgLink,
                    Id = category.Id
                });

        else return BadRequest("Failed to add category");
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCategory(int categoryId)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryAsync(categoryId);

        if (category == null) return NotFound("Category not found");

        if (await _unitOfWork.CategoryRepository.DeleteCategoryAsync(category))
            return Ok();

        return BadRequest("Failed to delete category");
    }

    [HttpPut]
    public async Task<ActionResult> UpdateCategory(CategoryDto categoryDto)
    {
        var category = await _unitOfWork.CategoryRepository.GetCategoryAsync(categoryDto.Id);

        if (category == null) return NotFound();

        _mapper.Map(categoryDto, category);
        _unitOfWork.CategoryRepository.UpdateCategory(category);

        if (await _unitOfWork.Complete()) return Ok();

        return BadRequest("Failed to update category");
    }
}
}