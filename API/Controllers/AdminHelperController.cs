using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
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

        [HttpPost("real-products")]
        public async Task<ActionResult<int>> AddRealProduct(RealProductDto realProductDto)
        {
            var realProduct  = new RealProduct();
            _mapper.Map(realProductDto, realProduct);

            _dataContext.RealProducts.Add(realProduct);

            await _dataContext.SaveChangesAsync();

            return Ok(realProduct.Id);
        }
        [HttpPut("real-products")]
        public async Task<ActionResult> UpdateRealProduct(RealProductDto realProductDto)
        {
            var realProduct  = new RealProduct();
            _mapper.Map(realProductDto, realProduct);

            _dataContext.Entry(realProduct).State = EntityState.Modified;

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("products")]
        public async Task<ActionResult<int>> AddProduct(AddProductDto productDto)
        {
            var product  = new Product();
            _mapper.Map(productDto, product);

            _dataContext.Products.Add(product);

            await _dataContext.SaveChangesAsync();

            return Ok(product.Id);
        }

        [HttpPut("products")]
        public async Task<ActionResult> UpdateProduct(AddProductDto productDto)
        {
            var product  =  new Product();
            _mapper.Map(productDto, product);

            var featuresBefore = await _dataContext.ProductFeatures
                                            .Where(pf => pf.ProductId == product.Id)
                                            .ToListAsync();
            
            // remove deleted features and update value existing features
            bool isExist = false;
            foreach(var featureBefore in featuresBefore)
            {
                isExist = false;
                foreach(var feature in product.ProductFeatures)
                {
                    if(featureBefore.FeatureId == feature.FeatureId)
                    {
                        feature.ProductId = product.Id;
                        featureBefore.Value = feature.Value;
                        isExist = true;
                        break;
                    }
                }
                if(!isExist)
                {
                    _dataContext.Remove(featureBefore);
                }
            }

            // add absolutely new features
            foreach(var feature in product.ProductFeatures)
            {
                if(feature.ProductId == 0)
                    _dataContext.ProductFeatures.Add(feature);
            }

            _dataContext.Entry(product).State = EntityState.Modified;
            

            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        // [HttpPost("photos")]
        // [HttpPost("add-photo")]
        // public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        // {
        //     var user = await _unitOfWork.UserRepository.GetUserByUserNameAsync(User.GetUserName());

        //     var result = await _photoService.AddPhotoAsync(file);

        //     if (result.Error != null) return BadRequest(result.Error.Message);

        //     var photo = new Photo
        //     {
        //         Url = result.SecureUrl.AbsoluteUri,
        //         PublicId = result.PublicId
        //     };

        //     // if (user.Photos.Count == 0)
        //     // {
        //     //     photo.IsMain = true;
        //     // }

        //     user.Photos.Add(photo);

        //     if (await _unitOfWork.Complete())
        //     {

        //         return CreatedAtRoute("GetUser", new { userName = user.UserName }, _mapper.Map<PhotoDto>(photo));

        //     }


        //     return BadRequest("Problem additing photo");
        // }
    }
}