using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminHelperController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DataContext _dataContext { get; }

        public AdminHelperController(IMapper mapper, IUnitOfWork unitOfWork, DataContext dataContext)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        [HttpGet("categories")]
        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetChildrenCategoriesAsync();
        }

        [HttpGet("features/{id}")]
        public async Task<ActionResult<IEnumerable<FeatureDto>>> GetFeatures(int id)
        {
            return await _unitOfWork.AdminRepository.GetFeatures(id);
        }

        [HttpPost("features")]
        public async Task<ActionResult<FeatureDto>> AddNewFeature(AddFeatureDto featureDto)
        {
            var feature = _mapper.Map<Feature>(featureDto);
            _unitOfWork.AdminRepository.AddFeature(feature);
            if (await _unitOfWork.Complete()) return Ok(feature);
            return BadRequest("Failed to add feature");
        }

        [HttpPost("real-products")]
        public async Task<ActionResult<int>> AddRealProduct(RealProductDto realProductDto)
        {
            var realProduct = new RealProduct();
            _mapper.Map(realProductDto, realProduct);

            _unitOfWork.AdminRepository.AddRealProduct(realProduct);

            if (await _unitOfWork.Complete()) return Ok(realProduct.Id);
            return BadRequest("Failed to add real product");
        }
        [HttpPut("real-products")]
        public async Task<ActionResult> UpdateRealProduct(RealProductDto realProductDto)
        {
            var realProduct = new RealProduct();
            _mapper.Map(realProductDto, realProduct);

            _unitOfWork.AdminRepository.UpdateRealProduct(realProduct);

            if (await _unitOfWork.Complete()) return Ok(realProduct);

            return BadRequest("Failed to update real product");
        }

        [HttpPost("products")]
        public async Task<ActionResult<int>> AddProduct(AddProductDto productDto)
        {
            var product = new Product();
            _mapper.Map(productDto, product);

            _unitOfWork.ProductRepository.AddProduct(product);

            if (await _unitOfWork.Complete()) return Ok(product.Id);

            return BadRequest("Failed to add product");
        }

        [HttpPut("products")]
        public async Task<ActionResult> UpdateProduct(AddProductDto productDto)
        {
            var product = new Product();
            _mapper.Map(productDto, product);

            var featuresBefore = await _unitOfWork.AdminRepository.GetPreviousFeatures(product.Id);

            // remove deleted features and update value existing features
            bool isExist = false;
            foreach (var featureBefore in featuresBefore)
            {
                isExist = false;
                foreach (var feature in product.ProductFeatures)
                {
                    if (featureBefore.FeatureId == feature.FeatureId)
                    {
                        feature.ProductId = product.Id;
                        featureBefore.Value = feature.Value;
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    _unitOfWork.AdminRepository.RemoveFeature(featureBefore);
                }
            }

            // add absolutely new features
            foreach (var feature in product.ProductFeatures)
            {
                if (feature.ProductId == 0)
                    _unitOfWork.AdminRepository.AddProductFeature(feature);
            }

            _unitOfWork.ProductRepository.UpdateProduct(product);


            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Failed to update product");

            
        }

        [HttpGet("search-orders/{searchString}")]
        public async Task<ActionResult<OrderDto>> SearchOrders(string searchString){
             var orders = await _dataContext.Orders
                .Include(p => p.OrderProducts)
                .ThenInclude(s => s.RealProduct)
                .Where(o => o.Id.ToString().Contains(searchString))
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(orders);
        }
    }
}