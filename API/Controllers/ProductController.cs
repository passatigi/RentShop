using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory([FromQuery] GetCategoryPageDto categoryPageDto)
        {
            return await _unitOfWork.ProductRepository.GetProductsByCategory(categoryPageDto.CategoryId);
        }

        [HttpGet("detail/{id}")]
        public async Task<ActionResult<DetailedProductDto>> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductById(id);
            
            if(product == null) return NotFound("Product not found");

            return Ok(product);
        }

        [HttpGet("shedule/{id}")]
        public async Task<RealProductSchedule> GetRealProductShedule(int id)
        {
            //would test it later
            return await _unitOfWork.ProductRepository.GetRealProductShedule(id);
        }
        
    }
}